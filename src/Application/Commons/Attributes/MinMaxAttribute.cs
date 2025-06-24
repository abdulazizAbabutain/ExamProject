namespace Application.Commons.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class MinMaxAttribute : Attribute
    {
        public int Min { get; }
        public int Max { get; }

        public MinMaxAttribute(int min,int max )
        {
            Min = min;
            Max = max;
        }
    }
}
