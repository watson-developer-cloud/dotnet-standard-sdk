using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdditionalPropertiesTest
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

            var json = JsonConvert.SerializeObject(x, new XConverter(typeof(X)));

            Assert.IsTrue(json == "{\"prop1\":\"string\",\"prop2\":42,\"myFoo\":{\"bar\":\"bar\",\"fum\":\"fum\",\"ack\":false},\"myFoo2\":{\"bar\":\"bar2\",\"fum\":\"fum2\",\"ack\":true}}");
            var x2 = JsonConvert.DeserializeObject<X>(json, new XConverter(typeof(X)));

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

            var json = JsonConvert.SerializeObject(y, new YConverter(typeof(Y)));
            Assert.IsTrue(json == "{\"prop1\":\"string\",\"prop2\":42,\"myFoo\":{\"bar\":\"bar\",\"fum\":\"fum\",\"ack\":false},\"baz\":\"baz\",\"qux\":1.23}");

            var y2 = JsonConvert.DeserializeObject<Y>(json, new YConverter(typeof(Y)));

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

    #region XConverter
    public class XConverter : JsonConverter
    {
        private readonly Type[] _types;

        public XConverter(params Type[] types)
        {
            _types = types;
        }

        public override bool CanConvert(Type objectType)
        {
            return _types.Any(t => t == objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            X x = new X();

            JObject item = JObject.Load(reader);

            if (!string.IsNullOrEmpty(item["prop1"].ToString()))
            {
                x.Prop1 = item["prop1"].ToString();
            }
            item.Remove("prop1");

            if (item["prop2"] != null)
            {
                long.TryParse(item["prop2"].ToString(), out long value);
                x.Prop2 = value;
            }
            item.Remove("prop2");

            IList<string> propertyNames = item.Properties().Select(p => p.Name).ToList();
            foreach (string prop in propertyNames)
            {
                var value = JsonConvert.DeserializeObject<Foo>(item[prop].ToString());
                x.Add(prop, value);
            }

            return x;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            JToken t = JToken.FromObject(value);

            if (t.Type != JTokenType.Object)
            {
                t.WriteTo(writer);
            }
            else
            {
                JObject o = (JObject)t;
                JToken jt = o.GetValue("AdditionalProperties");
                o.Remove("AdditionalProperties");
                JObject ojt = (JObject)jt;
                IList<string> propertyNames = ojt.Properties().Select(p => p.Name).ToList();
                foreach (string prop in propertyNames)
                {
                    o[prop] = ojt[prop];
                }

                o.WriteTo(writer);
            }
        }
    }
    #endregion

    public class Y : DynamicModel<object>
    {
        [JsonProperty("prop1", NullValueHandling = NullValueHandling.Ignore)]
        public string Prop1 { get; set; }
        [JsonProperty("prop2", NullValueHandling = NullValueHandling.Ignore)]
        public long Prop2 { get; set; }
    }

    #region YConverter
    public class YConverter : JsonConverter
    {
        private readonly Type[] _types;

        public YConverter(params Type[] types)
        {
            _types = types;
        }

        public override bool CanConvert(Type objectType)
        {
            return _types.Any(t => t == objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Y y = new Y();

            JObject item = JObject.Load(reader);

            if (!string.IsNullOrEmpty(item["prop1"].ToString()))
            {
                y.Prop1 = item["prop1"].ToString();
            }
            item.Remove("prop1");

            if (item["prop2"] != null)
            {
                long.TryParse(item["prop2"].ToString(), out long value);
                y.Prop2 = value;
            }
            item.Remove("prop2");

            IList<string> propertyNames = item.Properties().Select(p => p.Name).ToList();
            foreach (string prop in propertyNames)
            {
                switch (item[prop].Type)
                {
                    case JTokenType.String:
                        y.Add(prop, item[prop].ToString());
                        break;
                    case JTokenType.Float:
                        float.TryParse(item[prop].ToString(), out float value);
                        y.Add(prop, value);
                        break;
                    default:
                        var o = JsonConvert.DeserializeObject<JObject>(item[prop].ToString());
                        y.Add(prop, o);
                        break;
                }
            }

            return y;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            JToken t = JToken.FromObject(value);

            if (t.Type != JTokenType.Object)
            {
                t.WriteTo(writer);
            }
            else
            {
                JObject o = (JObject)t;
                JToken jt = o.GetValue("AdditionalProperties");
                o.Remove("AdditionalProperties");
                JObject ojt = (JObject)jt;
                IList<string> propertyNames = ojt.Properties().Select(p => p.Name).ToList();
                foreach (string prop in propertyNames)
                {
                    o[prop] = ojt[prop];
                }

                o.WriteTo(writer);
            }
        }
    }
    #endregion

    public class DynamicModel<T>
    {
        public Dictionary<string, T> AdditionalProperties { get; } = new Dictionary<string, T>();

        public void Add(string key, T value)
        {
            AdditionalProperties.Add(key, value);
        }

        public void Remove(string key)
        {
            AdditionalProperties.Remove(key);
        }

        public T Get(string key)
        {
            AdditionalProperties.TryGetValue(key, out T value);
            return value;
        }
    }
}
