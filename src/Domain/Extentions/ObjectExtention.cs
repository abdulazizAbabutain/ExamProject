using System.Runtime.CompilerServices;

namespace Domain.Extentions;

/// <summary>
/// Provides extension methods for null-checking on <see cref="object"/> instances.
/// </summary>
public static class ObjectExtension
{
    /// <summary>
    /// Determines whether the specified object is not <c>null</c>.
    /// </summary>
    /// <param name="value">The object to check.</param>
    /// <summary>
        /// Determines whether the specified object is not null.
        /// </summary>
        /// <param name="value">The object to check for null.</param>
        /// <returns><c>true</c> if the object is not null; otherwise, <c>false</c>.</returns>
    public static bool IsNotNull(this object? value)
        => value is not null;

    /// <summary>
    /// Determines whether the specified object is <c>null</c>.
    /// </summary>
    /// <param name="value">The object to check.</param>
    /// <summary>
       /// Determines whether the specified object is null.
       /// </summary>
       /// <param name="value">The object to check for null.</param>
       /// <returns><c>true</c> if the object is null; otherwise, <c>false</c>.</returns>
    public static bool IsNull(this object? value)
       => value is null;
}
