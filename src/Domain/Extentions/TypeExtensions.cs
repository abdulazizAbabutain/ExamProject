using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Extentions;

public static class TypeExtensions
{
    public static string GetFriendlyName(this Type type)
    {
        // If it's a generic type, we need to process generic arguments
        if (type.IsGenericType)
        {
            // For example: "List`1" -> "List"
            string typeNameWithoutArity = type.Name.Substring(0, type.Name.IndexOf('`'));

            // Recursively get friendly names for generic arguments
            var genericArgs = type.GetGenericArguments()
                .Select(t => t.GetFriendlyName());

            // Join them with commas inside angle brackets
            return $"{typeNameWithoutArity}<{string.Join(", ", genericArgs)}>";
        }
        else
        {
            // Non-generic type: just return the Name
            return type.Name;
        }
    }
}
