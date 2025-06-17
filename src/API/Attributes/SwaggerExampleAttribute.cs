namespace API.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class SwaggerExampleAttribute : Attribute
    {
        public object Example { get; }
        public SwaggerExampleAttribute(object example)
        {
            Example = example;
        }
    }
}
