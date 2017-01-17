using System;

namespace IBM.WatsonDeveloperCloud.Util.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class DescriptionAttribute : Attribute
    {
        public string Description { get; protected set; }

        public DescriptionAttribute(string description)
        {
            Description = description;
        }
    }
}