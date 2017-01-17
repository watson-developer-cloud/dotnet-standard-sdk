using System;
using System.Linq;
using System.Reflection;
using IBM.WatsonDeveloperCloud.Util.Attributes;

namespace IBM.WatsonDeveloperCloud.Util.Extensions
{
    public static class DescriptionExtension
    {
        public static string Description(this Enum value)
        {
            DescriptionAttribute attribute = value.GetType()
                .GetField(value.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .SingleOrDefault() as DescriptionAttribute;

            return attribute == null ? value.ToString() : attribute.Description;
        }
    }
}