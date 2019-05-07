/**
* Copyright 2019 IBM Corp. All Rights Reserved.
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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace IBM.Watson.IntegrationTests
{
    [TestClass]
    public class AdditionalPropertiesTest
    {
        [TestMethod]
        public void TestRestrictedDynamicModel()
        {
            X x = new X();
            x.Prop1 = "string";
            x.Prop2 = 42;

            Foo foo = new Foo();
            foo.Bar = "bar";
            foo.Fum = "fum";
            foo.Ack = false;
            x.Add("myFoo", foo);

            Foo foo2 = new Foo();
            foo2.Bar = "bar2";
            foo2.Fum = "fum2";
            foo2.Ack = true;
            x.Add("myFoo2", foo2);

            var json = JsonConvert.SerializeObject(x);

            Assert.IsTrue(json == "{\"prop1\":\"string\",\"prop2\":42,\"myFoo\":{\"bar\":\"bar\",\"fum\":\"fum\",\"ack\":false},\"myFoo2\":{\"bar\":\"bar2\",\"fum\":\"fum2\",\"ack\":true}}");
            var x2 = JsonConvert.DeserializeObject<X>(json);

            Assert.IsTrue(x2.Prop1 == "string");
            Assert.IsTrue(x2.Prop2 == 42);
            Assert.IsTrue(x2.Get("myFoo").Bar == "bar");
            Assert.IsTrue(x2.Get("myFoo").Fum == "fum");
            Assert.IsTrue(x2.Get("myFoo").Ack == false);
            Assert.IsTrue(x2.Get("myFoo2").Bar == "bar2");
            Assert.IsTrue(x2.Get("myFoo2").Fum == "fum2");
            Assert.IsTrue(x2.Get("myFoo2").Ack == true);
        }

        [TestMethod]
        public void TestUnrestrictedDynamicModel()
        {
            Y y = new Y();
            y.Prop1 = "string";
            y.Prop2 = 42;

            Foo foo = new Foo();
            foo.Bar = "bar";
            foo.Fum = "fum";
            foo.Ack = false;

            y.Add("myFoo", foo);
            y.Add("baz", "baz");
            y.Add("qux", 1.23f);

            var json = JsonConvert.SerializeObject(y);
            Assert.IsTrue(json == "{\"prop1\":\"string\",\"prop2\":42,\"myFoo\":{\"bar\":\"bar\",\"fum\":\"fum\",\"ack\":false},\"baz\":\"baz\",\"qux\":1.23}");

            var y2 = JsonConvert.DeserializeObject<Y>(json);

            Assert.IsTrue(y2.Prop1 == "string");

            Assert.IsTrue(y2.Prop2 == 42);

            var foo2 = y2.Get("myFoo");
            var foo2Obj = JObject.FromObject(foo2);
            var bar = foo2Obj["bar"].ToString();
            var fum = foo2Obj["fum"].ToString();
            var ack = (bool)foo2Obj["ack"];

            Assert.IsTrue(bar == "bar");
            Assert.IsTrue(fum == "fum");
            Assert.IsTrue(ack == false);

            Assert.IsTrue(y2.Get("baz").ToString() == "baz");

            float.TryParse(y2.Get("qux").ToString(), out float qux);
            Assert.IsTrue(qux == 1.23f);
        }
    }

    public class X : DynamicModel<Foo>
    {
        [JsonProperty("prop1", NullValueHandling = NullValueHandling.Ignore)]
        public string Prop1 { get; set; }
        [JsonProperty("prop2", NullValueHandling = NullValueHandling.Ignore)]
        public long Prop2 { get; set; }
    }

    public class Foo
    {
        [JsonProperty("bar", NullValueHandling = NullValueHandling.Ignore)]
        public string Bar { get; set; }
        [JsonProperty("fum", NullValueHandling = NullValueHandling.Ignore)]
        public string Fum { get; set; }
        [JsonProperty("ack", NullValueHandling = NullValueHandling.Ignore)]
        public bool Ack { get; set; }
    }

    public class Y : DynamicModel
    {
        [JsonProperty("prop1", NullValueHandling = NullValueHandling.Ignore)]
        public string Prop1 { get; set; }
        [JsonProperty("prop2", NullValueHandling = NullValueHandling.Ignore)]
        public long Prop2 { get; set; }
    }

    public class DynamicModel<T>
    {
        [JsonExtensionData]
        public Dictionary<string, JToken> AdditionalProperties { get; } = new Dictionary<string, JToken>();

        public void Add(string key, T value)
        {
            AdditionalProperties.Add(key, JToken.FromObject(value));
        }

        public void Remove(string key)
        {
            AdditionalProperties.Remove(key);
        }

        public T Get(string key)
        {
            AdditionalProperties.TryGetValue(key, out JToken value);
            return value.ToObject<T>();
        }
    }

    public class DynamicModel
    {
        [JsonExtensionData]
        public Dictionary<string, JToken> AdditionalProperties { get; } = new Dictionary<string, JToken>();

        public void Add(string key, object value)
        {
            AdditionalProperties.Add(key, JToken.FromObject(value));
        }

        public void Remove(string key)
        {
            AdditionalProperties.Remove(key);
        }

        public JToken Get(string key)
        {
            AdditionalProperties.TryGetValue(key, out JToken value);
            return value;
        }
    }
}
