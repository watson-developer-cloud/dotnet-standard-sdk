using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdditionalPropertiesTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestRestrictedDynamicModel()
        {
            X x = new X();
            x.Prop1 = "string";
            x.Prop2 = 42;

            Foo foo = new Foo();
            foo.Bar = "bar";

            x.Add("myFoo", foo);

            Foo foo2 = new Foo();
            foo2.Bar = "bar2";
            x.Add("myFoo2", foo2);

            var json = JsonConvert.SerializeObject(x, new XConverter(typeof(X)));

            Assert.IsTrue(json == "{\"prop1\":\"string\",\"prop2\":42,\"myFoo\":{\"bar\":\"bar\"},\"myFoo2\":{\"bar\":\"bar2\"}}");

            var x2 = JsonConvert.DeserializeObject<X>(json, new XConverter(typeof(X)));

            Assert.IsTrue(x2.Prop1 == "string");
            Assert.IsTrue(x2.Prop2 == 42);
            Assert.IsTrue(x2.Get("myFoo").Bar == "bar");
            Assert.IsTrue(x2.Get("myFoo2").Bar == "bar2");
        }

        [TestMethod]
        public void TestUnrestrictedDynamicModel()
        {
            Y y = new Y();
            y.Prop1 = "string";
            y.Prop2 = 42;

            Foo foo = new Foo();
            foo.Bar = "bar";

            y.Add("myFoo", foo);
            y.Add("baz", "baz");
            y.Add("qux", 1.00f);

            var json = JsonConvert.SerializeObject(y, new YConverter(typeof(Y)));
            Assert.IsTrue(json == "{\"prop1\":\"string\",\"prop2\":42,\"myFoo\":{\"bar\":\"bar\"},\"baz\":\"baz\",\"qux\":1.0}");

            var y2 = JsonConvert.DeserializeObject<Y>(json, new YConverter(typeof(Y)));
            Assert.IsTrue(y2.Prop1 == "string");
            Assert.IsTrue(y2.Prop2 == 42);
            var foo2 = y2.Get("myFoo");
            //IDictionary<string, string> fooDict = (IDictionary<string, string>)foo2;
            //fooDict.TryGetValue("bar", out string bar);
            //Assert.IsTrue(bar == "bar");
            //var item = y2.Get("myFoo");
            //var type = item.GetType();
            //var prop = type.GetProperty("bar");
            //System.Reflection.PropertyInfo pi = item.GetType().GetProperty("bar");
            //string bar = (string)(pi.GetValue(item));
            var bar = Util.GetPropertyValue(foo2, "bar");
            Assert.IsTrue(bar.ToString() == "bar");
            Assert.IsTrue(y2.Get("baz").ToString() == "baz");
            float.TryParse(y2.Get("qux").ToString(), out float qux);
            Assert.IsTrue(qux == 1.00f);
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
                x.AdditionalProperties.Add(prop, value);
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

    public class Y : DynamicModel
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
                        y.AdditionalProperties.Add(prop, item[prop].ToString());
                        break;
                    case JTokenType.Float:
                        float.TryParse(item[prop].ToString(), out float value);
                        y.AdditionalProperties.Add(prop, value);
                        break;
                    default:
                        var o = JsonConvert.DeserializeObject<object>(item[prop].ToString());
                        y.AdditionalProperties.Add(prop, o);
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
        public Dictionary<string, T> AdditionalProperties { get; set; } = new Dictionary<string, T>();

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
    
    public class DynamicModel
    {
        public Dictionary<string, object> AdditionalProperties { get; set; } = new Dictionary<string, object>();

        public void Add(string key, object value)
        {
            AdditionalProperties.Add(key, value);
        }

        public void Remove(string key)
        {
            AdditionalProperties.Remove(key);
        }

        public object Get(string key)
        {
            AdditionalProperties.TryGetValue(key, out object value);
            return value;
        }
    }

    public class Util
    {
        public static object GetPropertyValue(object src, string propName)
        {
            if (src == null) throw new ArgumentException("Value cannot be null.", "src");
            if (propName == null) throw new ArgumentException("Value cannot be null.", "propName");

            if (propName.Contains("."))//complex type nested
            {
                var temp = propName.Split(new char[] { '.' }, 2);
                return GetPropertyValue(GetPropertyValue(src, temp[0]), temp[1]);
            }
            else
            {
                var prop = src.GetType().GetProperty(propName);
                return prop != null ? prop.GetValue(src, null) : null;
            }
        }
    }
}
