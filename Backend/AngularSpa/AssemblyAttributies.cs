namespace Backend.AngularSpa
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public class SpaRootAttribute : Attribute
    {
        public string Value { get; set; }

        public SpaRootAttribute(string value)
        {
            Value = value;
        }
    }

    [AttributeUsage(AttributeTargets.Assembly)]
    public class SpaDistBuildAttribute : Attribute
    {
        public string Value { get; set; }

        public SpaDistBuildAttribute(string value)
        {
            Value = value;
        }
    }

    [AttributeUsage(AttributeTargets.Assembly)]
    public class SpaPublishedAttribute : Attribute
    {
        public string Value { get; set; }

        public SpaPublishedAttribute(string value)
        {
            Value = value;
        }
    }
}