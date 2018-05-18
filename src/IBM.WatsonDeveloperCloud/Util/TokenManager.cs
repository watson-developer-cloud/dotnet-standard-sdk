/**
* Copyright 2017 IBM Corp. All Rights Reserved.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*      http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*
*/

using IBM.WatsonDeveloperCloud.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace IBM.WatsonDeveloperCloud.Util
{
    public class TokenManager
    {
        public IClient Client { get; set; }

        private string _iamUrl;
        private IamTokenData _tokenInfo;
        private string _iamApikey;
        private string _userAccessToken;

        public TokenManager(TokenOptions options)
        {
            _iamUrl = string.IsNullOrEmpty(options.IamUrl) ? "https://iam.ng.bluemix.net/identity/token" : options.IamUrl;
            _tokenInfo = new IamTokenData();
            if (!string.IsNullOrEmpty(options.IamApiKey))
                _iamApikey = options.IamApiKey;
            if (!string.IsNullOrEmpty(options.IamAccessToken))
                _userAccessToken = options.IamAccessToken;

            this.Client = new WatsonHttpClient(this._iamUrl);
        }

        /// <summary>
        /// This function sends an access token back through a callback. The source of the token
        /// is determined by the following logic:
        /// 1. If user provides their own managed access token, assume it is valid and send it
        /// 2. If this class is managing tokens and does not yet have one, make a request for one
        /// 3. If this class is managing tokens and the token has expired, refresh it
        /// 4. If this class is managing tokens and has a valid token stored, send it
        /// </summary>
        /// <returns>An IamTokenData object containing the IAM token.</returns>
        public string GetToken()
        {
            if (!string.IsNullOrEmpty(_userAccessToken))
            {
                // 1. use user-managed token
                return _userAccessToken;
            }
            else if (!string.IsNullOrEmpty(_tokenInfo.AccessToken) || IsRefreshTokenExpired())
            {
                // 2. request an initial token
                var tokenInfo = RequestToken();
                SaveTokenInfo(tokenInfo);
                return _tokenInfo.AccessToken;
            }
            else if (this.IsTokenExpired())
            {
                // 3. refresh a token
                var tokenInfo = RefreshToken();
                SaveTokenInfo(tokenInfo);
                return _tokenInfo.AccessToken;
            }
            else
            {
                // 4. use valid managed token
                return _tokenInfo.AccessToken;
            }
        }

        /// <summary>
        /// Set a self-managed IAM access token.
        /// The access token should be valid and not yet expired.
        /// 
        /// By using this method, you accept responsibility for managing the
        /// access token yourself. You must set a new access token before this
        /// one expires. Failing to do so will result in authentication errors
        /// after this token expires.
        /// </summary>
        /// <param name="iamAccessToken">A valid, non-expired IAM access token</param>
        public void SetAccessToken(string iamAccessToken)
        {
            _userAccessToken = iamAccessToken;
        }

        /// <summary>
        /// Request an IAM token using an API key.
        /// </summary>
        /// <returns>An IamTokenData object containing the IAM token.</returns>
        private IamTokenData RequestToken()
        {
            IamTokenData result = null;

            try
            {
                if (string.IsNullOrEmpty(_iamApikey))
                    throw new ArgumentNullException(nameof(_iamApikey));

                var request = this.Client.PostAsync(_iamUrl);
                request.WithHeader("Content-type", "application/x-www-form-urlencoded");
                request.WithHeader("Authorization", "Basic Yng6Yng=");

                List<KeyValuePair<string, string>> content = new List<KeyValuePair<string, string>>();
                KeyValuePair<string, string> grantType = new KeyValuePair<string, string>("grant_type", "urn:ibm:params:oauth:grant-type:apikey");
                KeyValuePair<string, string> responseType = new KeyValuePair<string, string>("response_type", "cloud_iam");
                KeyValuePair<string, string> apikey = new KeyValuePair<string, string>("apikey", _iamApikey);
                content.Add(grantType);
                content.Add(responseType);
                content.Add(apikey);

                var formData = new FormUrlEncodedContent(content);

                request.WithBodyContent(formData);

                result = request.As<IamTokenData>().Result;

                if (result == null)
                    result = new IamTokenData();
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Refresh an IAM token using a refresh token.
        /// </summary>
        private IamTokenData RefreshToken()
        {
            IamTokenData result = null;

            try
            {
                if (string.IsNullOrEmpty(_tokenInfo.RefreshToken))
                    throw new ArgumentNullException(nameof(_tokenInfo.RefreshToken));

                var request = this.Client.PostAsync(_iamUrl);
                request.WithHeader("Content-type", "application/x-www-form-urlencoded");
                request.WithHeader("Authorization", "Basic Yng6Yng=");

                List<KeyValuePair<string, string>> content = new List<KeyValuePair<string, string>>();
                KeyValuePair<string, string> grantType = new KeyValuePair<string, string>("grant_type", "refresh_token");
                KeyValuePair<string, string> refreshToken = new KeyValuePair<string, string>("refresh_token", _tokenInfo.RefreshToken);
                content.Add(grantType);
                content.Add(refreshToken);
                
                var formData = new FormUrlEncodedContent(content);
                
                request.WithBodyContent(formData);

                result = request.As<IamTokenData>().Result;

                if (result == null)
                    result = new IamTokenData();
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Check if currently stored token is expired.
        /// 
        /// Using a buffer to prevent the edge case of the 
        /// token expiring before the request could be made.
        /// 
        /// The buffer will be a fraction of the total TTL. Using 80%.
        /// </summary>
        /// <returns>true if the token has expired, false if it hasn't.</returns>
        private bool IsTokenExpired()
        {
            if (_tokenInfo.ExpiresIn == null || _tokenInfo.Expiration == null)
                return true;

            float fractionOfTtl = 0.8f;
            long? timeToLive = _tokenInfo.ExpiresIn;
            long? expireTime = _tokenInfo.Expiration;
            long currentTime = DateTimeOffset.Now.ToUnixTimeSeconds();

            double? refreshTime = expireTime - (timeToLive * (1.0 - fractionOfTtl));
            return refreshTime < currentTime;
        }

        /// <summary>
        ///  Used as a fail-safe to prevent the condition of a refresh token expiring,
        ///  which could happen after around 30 days. This function will return true
        ///  if it has been at least 7 days and 1 hour since the last token was
        ///  retrieved.
        /// </summary>
        /// <returns>true if the refresh token has expired, false if it hasn't.</returns>
        private bool IsRefreshTokenExpired()
        {
            if (_tokenInfo.Expiration == null)
            {
                return true;
            };

            long sevenDays = 7 * 24 * 3600;
            long currentTime = DateTimeOffset.Now.ToUnixTimeSeconds();
            long? newTokenTime = _tokenInfo.Expiration + sevenDays;
            return newTokenTime < currentTime;
        }

        /// <summary>
        /// Save the response from the IAM service request to the object's state.
        /// </summary>
        /// <param name="tokenResponse"></param>
        private void SaveTokenInfo(IamTokenData tokenResponse)
        {
            _tokenInfo = tokenResponse;
        }
    }
}
