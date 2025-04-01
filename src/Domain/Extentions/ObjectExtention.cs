using System.Runtime.CompilerServices;

namespace Domain.Extentions
{
    public static class ObjectExtention
    {
        public static bool IsNotNull(this object value)
            => value is not null;


        public static bool IsNull(this object value)
           => value is null;
    }
}
