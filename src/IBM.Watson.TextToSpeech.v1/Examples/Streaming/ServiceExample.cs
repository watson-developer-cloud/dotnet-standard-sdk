/**
* (C) Copyright IBM Corp. 2021.
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

using IBM.Cloud.SDK.Core.Authentication.Iam;
using IBM.Watson.TextToSpeech.v1.Websockets;
using LibVLCSharp.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Threading;
using System.Threading.Tasks;
using static IBM.Watson.TextToSpeech.v1.TextToSpeechService;

namespace IBM.Watson.TextToSpeech.v1.Examples.Streaming
{
    public class StreamingExample
    {
        string apikey = "{apikey}";
        string url = "{serviceUrl}";
        string customizationId;

        string filepath = "C:\\Users\\ajiemar\\Downloads\\Unity-Watson-STT-Assistant-TTS-master (1)\\Unity-Watson-STT-Assistant-TTS-master\\Assets\\unity-sdk-4.8.0\\Tests\\TestData\\SpeechToTextV1\\test-audio.wav";
        string testText = "Jabberwocky is a nonsense poem written by Lewis Carroll about the killing of a creature named the Jabberwock. It was included in his 1871 novel Through the Looking-Glass, and What Alice Found There, the sequel to Alice's Adventures in Wonderland (1865). The book tells of Alice's adventures within the back-to-front world of Looking-Glass Land. In an early scene in which she first encounters the chess piece characters White King and White Queen, Alice finds a book written in a seemingly unintelligible language. Realizing that she is travelling through an inverted world, she recognises that the verses on the pages are written in mirror-writing. She holds a mirror to one of the poems and reads the reflected verse of Jabberwocky. She finds the nonsense verse as puzzling as the odd land she has passed into, later revealed as a dreamscape.";


        static void Main(string[] args)
        {
            StreamingExample example = new StreamingExample();
            example.SynthesizeUsingWebsockets();

            //example.TestVlanFile();
            //example.TestVlanMemoryStream();
            //example.TestSynthesizeUsingWebsockets();

            //await example.TestSynthesizeUsingWebsocketsAsync();
        }
        #region junk
        #region testVlanFile
        //public void TestVlanFile()
        //{
        //    Core.Initialize();

        //    var libvlc = new LibVLC(enableDebugLogs: true);
        //    var media = new Media(libvlc, new Uri(@filepath));
        //    var mediaPlayer = new MediaPlayer(media);

        //    mediaPlayer.Play();
        //    Console.ReadKey();
        //}
        #endregion

        #region testVlanMemoryStream
        //public void TestVlanMemoryStream()
        //{
        //    Core.Initialize();

        //    using (FileStream fs = File.OpenRead(filepath))
        //    {
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            fs.CopyTo(ms);

        //            var libvlc = new LibVLC(enableDebugLogs: true);
        //            var mediaInput = new StreamMediaInput(ms);
        //            var media = new Media(libvlc, mediaInput);
        //            var mediaPlayer = new MediaPlayer(media);

        //            mediaPlayer.Play();
        //            Console.ReadKey();
        //        }
        //    }
        //}
        #endregion

        #region TestSynthesizeUsingWebsockets
        //public void TestSynthesizeUsingWebsockets()
        //{
        //    Core.Initialize();

        //    IamAuthenticator authenticator = new IamAuthenticator(
        //        apikey: apikey);

        //    TextToSpeechService service = new TextToSpeechService(authenticator);
        //    service.SetServiceUrl(url);

        //    SynthesizeCallback callback = new SynthesizeCallback();

        //    MemoryStream soundStream = new MemoryStream();

        //    var libvlc = new LibVLC(enableDebugLogs: false);
        //    var mediaInput = new StreamMediaInput(soundStream);
        //    var media = new Media(libvlc, mediaInput);
        //    var mediaPlayer = new MediaPlayer(media);

        //    callback.OnOpen = () =>
        //    {
        //        Console.WriteLine("OnOpen");
        //    };

        //    callback.OnClose = () =>
        //    {
        //        Console.WriteLine("OnClose");
        //    };
        //    callback.OnMessage = (bytes) =>
        //    {
        //        Console.WriteLine("OnMessage");
        //        soundStream.Position = soundStream.Length;
        //        soundStream.WriteAsync(bytes, 0, bytes.Length);
        //        WaveUtils.ReWriteWaveHeader(soundStream);
        //    };

        //    var synthesizeResult = Task.Run(() => service.SynthesizeUsingWebsockets(
        //        voice: SynthesizeEnums.VoiceValue.EN_US_ALLISONVOICE,
        //        callback: callback,
        //        accept: SynthesizeEnums.AcceptValue.AUDIO_WAV,
        //        timings: new string[] { "words" },
        //        text: testText));

        //    Console.WriteLine("************************************************************************* playing");
        //    mediaPlayer.Play();

        //    Console.ReadKey();
        //}

        //public void PlayAudioFromStream(MemoryStream stream)
        //{
        //    using (Stream ms = new MemoryStream())
        //    {

        //        byte[] buffer = new byte[32768];
        //        int read;
        //        while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
        //        {
        //            ms.Write(buffer, 0, read);
        //        }

        //        ms.Position = 0;
        //        //using (WaveStream blockAlignedStream =
        //        //    new BlockAlignReductionStream(
        //        //        WaveFormatConversionStream.CreatePcmStream(
        //        //            new Mp3FileReader(ms))))
        //        //{
        //        //    using (WaveOut waveOut = new WaveOut(WaveCallbackInfo.FunctionCallback()))
        //        //    {
        //        //        waveOut.Init(blockAlignedStream);
        //        //        waveOut.PlaybackStopped += (sender, e) =>
        //        //        {
        //        //            waveOut.Stop();
        //        //        };
        //        //        waveOut.Play();
        //        //        waiting = true;
        //        //        stop.WaitOne(timeout);
        //        //        waiting = false;
        //        //    }
        //        //}
        //    }
        //}
        #endregion

        #region TestSynthesizeUsingWebsocketsAsync
        //public async Task TestSynthesizeUsingWebsocketsAsync()
        //{
        //    Core.Initialize();

        //    IamAuthenticator authenticator = new IamAuthenticator(
        //        apikey: apikey);

        //    TextToSpeechService service = new TextToSpeechService(authenticator);
        //    service.SetServiceUrl(url);

        //    SynthesizeCallback callback = new SynthesizeCallback();

        //    MemoryStream soundStream = new MemoryStream();

        //    var libvlc = new LibVLC(enableDebugLogs: true);
        //    var mediaInput = new StreamMediaInput(soundStream);
        //    var media = new Media(libvlc, mediaInput);
        //    var mediaPlayer = new MediaPlayer(media);

        //    callback.OnOpen = () =>
        //    {
        //        Console.WriteLine("OnOpen");
        //    };

        //    callback.OnClose = () =>
        //    {
        //        Console.WriteLine("OnClose");
        //    };
        //    callback.OnMessage = (bytes) =>
        //    {
        //        soundStream.Position = soundStream.Length;
        //        soundStream.WriteAsync(bytes, 0, bytes.Length);
        //        WaveUtils.ReWriteWaveHeader(soundStream);
        //        Console.WriteLine("OnMessage");
        //    };

        //    var synthesizeResult = service.SynthesizeUsingWebsockets(
        //        voice: SynthesizeEnums.VoiceValue.EN_US_ALLISONVOICE,
        //        callback: callback,
        //        accept: SynthesizeEnums.AcceptValue.AUDIO_WAV,
        //        timings: new string[] { "words" },
        //        text: testText);

        //    mediaPlayer.Play();
        //    Console.ReadKey();
        //}
        #endregion
        #endregion
        #region SynthesizeUsingWebsockets
        public void SynthesizeUsingWebsockets()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: apikey);

            TextToSpeechService service = new TextToSpeechService(authenticator);
            service.SetServiceUrl(url);

            SynthesizeCallback callback = new SynthesizeCallback();

            MemoryStream soundStream = new MemoryStream();
            SoundPlayer player = new SoundPlayer(soundStream);

            callback.OnOpen = () =>
            {
                Console.WriteLine("OnOpen()");
            };

            callback.OnClose = () =>
            {
                Console.WriteLine("OnClose()");
            };

            callback.OnMessage = (bytes) =>
            {
                player.Stream.Position = player.Stream.Length;
                player.Stream.WriteAsync(bytes, 0, bytes.Length);
                WaveUtils.ReWriteWaveHeader((MemoryStream)player.Stream);
                Console.WriteLine(String.Format("OnMessage({0})", player.Stream.Length));
            };

            callback.onTimings = (wordTimings) =>
            {
                foreach(List<string> word in wordTimings.Words)
                {
                    Console.WriteLine(String.Format("OnTimings({0}, {1}, {2})", word[0], word[1], word[2]));
                }
            };

            var synthesizeResult = Task.Run(() => service.SynthesizeUsingWebsockets(
                voice: SynthesizeEnums.VoiceValue.EN_US_ALLISONVOICE,
                callback: callback,
                accept: SynthesizeEnums.AcceptValue.AUDIO_WAV,
                text: testText));

            //  buffer
            while(soundStream.Length < 65536 * 8)
            {
                Thread.Sleep(1000);
            }

            Task.Run(() => player.Play());

            Console.ReadKey();
        }
        #endregion

        #region SynthesizeUsingWebsockets2
        public void SynthesizeUsingWebsockets2()
        {
            Core.Initialize();

            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: apikey);

            TextToSpeechService service = new TextToSpeechService(authenticator);
            service.SetServiceUrl(url);

            SynthesizeCallback callback = new SynthesizeCallback();

            MemoryStream soundStream = new MemoryStream();

            var libvlc = new LibVLC(enableDebugLogs: false);
            var mediaInput = new StreamMediaInput(soundStream);
            var media = new Media(libvlc, mediaInput);
            var mediaPlayer = new MediaPlayer(media);

            callback.OnOpen = () =>
            {
                Console.WriteLine("OnOpen()");
            };

            callback.OnClose = () =>
            {
                Console.WriteLine("OnClose()");
                //var pos = mediaPlayer.Position;
                //WaveUtils.ReWriteWaveHeader(soundStream);
                //mediaPlayer.Position = pos;
                //mediaPlayer.Play();
            };

            callback.OnMessage = (bytes) =>
            {
                soundStream.Position = soundStream.Length;
                soundStream.WriteAsync(bytes, 0, bytes.Length);
                WaveUtils.ReWriteWaveHeader(soundStream);
                Console.WriteLine(String.Format("OnMessage({0})", soundStream.Length));
            };

            callback.onTimings = (wordTimings) =>
            {
                foreach (List<string> word in wordTimings.Words)
                {
                    Console.WriteLine(String.Format("OnTimings({0}, {1}, {2})", word[0], word[1], word[2]));
                }
            };

            var synthesizeResult = Task.Run(() => service.SynthesizeUsingWebsockets(
                voice: SynthesizeEnums.VoiceValue.EN_US_ALLISONV3VOICE,
                callback: callback,
                accept: SynthesizeEnums.AcceptValue.AUDIO_WAV,
                text: testText));

            //  buffer
            while (soundStream.Length < 65536 * 5)
            {
                Thread.Sleep(1000);
            }

            Task.Run(() => mediaPlayer.Play());

            Console.ReadKey();
        }
        #endregion
    }
}
