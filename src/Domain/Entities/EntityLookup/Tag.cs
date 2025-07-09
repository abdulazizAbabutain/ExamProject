using Ardalis.GuardClauses;
using Domain.Auditing;
using Domain.Constants;
using Domain.Entities.Metadata;
using Domain.Enums;
using Domain.Extentions;

namespace Domain.Entities.EntityLookup;

/// <summary>
/// Represents a categorized tag entity with color properties and archiving support.
/// </summary>
/// <remarks>
/// The <see cref="Tag"/> class supports creation, updating, and soft-archiving functionality.
/// It enforces domain rules to ensure that all tags have valid names and color codes in hexadecimal format.
/// </remarks>
public class Tag : EntityAudit
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Tag"/> class with the specified name and color codes.
    /// </summary>
    /// <param name="name">The name of the tag. Must be non-empty and non-null.</param>
    /// <param name="backgroundColorHexCode">
    /// The background color of the tag in hexadecimal format (e.g., <c>#FFFFFF</c>). Must be a valid 6-digit hex code with a leading hash.
    /// </param>
    /// <param name="textColorCode">
    /// The optional text color of the tag in hexadecimal format (e.g., <c>#000000</c>). 
    /// If not provided, the default color <see cref="ColorsConsts.White"/> will be used.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="name"/>, <paramref name="backgroundColorHexCode"/>, or resolved <paramref name="textColorCode"/> is null or empty.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Thrown if <paramref name="backgroundColorHexCode"/> or <paramref name="textColorCode"/> are not valid hex color codes (must match format <c>#RRGGBB</c>).
    /// </exception>
    public Tag(string name, string backgroundColorHexCode, string textColorCode = null, string iconPath = null, string iconColorCode = null) 
    {
        var textColor = textColorCode ?? ColorsConsts.White;

        Id = Guid.CreateVersion7();
        Name = Guard.Against.NullOrEmpty(name);
        BackgroundColorCode = Guard.Against.NullOrEmpty(backgroundColorHexCode);
        TextColorCode = Guard.Against.NullOrEmpty(textColor);

        if (!BackgroundColorCode.IsHexColor())
            throw new ArgumentException("Invalid hex color for background", nameof(backgroundColorHexCode));

        if (!TextColorCode.IsHexColor())
            throw new ArgumentException("Invalid hex color for text", nameof(textColorCode));

        if (iconPath != null)
        {
            var iconColor = iconColorCode ?? ColorsConsts.White;
            Icon = new IconMetadata(iconPath.GetOriginalNameFromFile(),iconPath, iconColor);
        }

        BackgroundColorGroup = BackgroundColorCode.GetColorGroup();
        TextColorGroup = TextColorCode.GetColorGroup();

        Created();
    }

    /// <summary>
    /// Updates the current <see cref="Tag"/> with a new name, background color, and text color.
    /// </summary>
    /// <param name="name">The new name of the tag. Must not be null or empty.</param>
    /// <param name="backgroundColorHexCode">
    /// The new background color in hexadecimal format (e.g., <c>#FF5733</c>). Must be a valid 6-digit hex code with a leading hash.
    /// </param>
    /// <param name="textColorCode">
    /// The new text color in hexadecimal format (e.g., <c>#000000</c>). Must be a valid 6-digit hex code with a leading hash.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="name"/>, <paramref name="backgroundColorHexCode"/>, or <paramref name="textColorCode"/> is null or empty.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Thrown if <paramref name="backgroundColorHexCode"/> or <paramref name="textColorCode"/> are not valid hex color codes (must match format <c>#RRGGBB</c>).
    /// </exception>
    public void UpdateTag(string name, string backgroundColorHexCode, string textColorCode)
    {
        Name = Guard.Against.NullOrEmpty(name);
        BackgroundColorCode = Guard.Against.NullOrEmpty(backgroundColorHexCode);
        TextColorCode = Guard.Against.NullOrEmpty(textColorCode);

        if (!BackgroundColorCode.IsHexColor())
            throw new ArgumentException("Invalid hex color for background", nameof(backgroundColorHexCode));

        if (!TextColorCode.IsHexColor())
            throw new ArgumentException("Invalid hex color for text", nameof(textColorCode));

        BackgroundColorGroup = BackgroundColorCode.GetColorGroup();
        TextColorGroup = TextColorCode.GetColorGroup();
        Updated();
    }
    
    /// <summary>
    /// Archives the tag by marking it as no longer active.
    /// </summary>
    /// <remarks>
    /// This method sets <see cref="EntityAudit.IsArchived"/> to <c>true</c> and records the archive timestamp.
    /// </remarks>
    public void ArchiveTag() => Archive();
    
    /// <summary>
    /// Restores the tag from an archived state.
    /// </summary>
    /// <remarks>
    /// This method sets <see cref="EntityAudit.IsArchived"/> to <c>false</c> and updates modification metadata.
    /// </remarks>
    public void UnArchiveTag() => UnArchive();

    /// <summary>
    /// Gets the unique identifier of the tag.
    /// </summary>
    public Guid Id { get; private set; }
    
    /// <summary>
    /// Gets the name of the tag.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Gets the background color code of the tag in hexadecimal format.
    /// </summary>
    public string BackgroundColorCode { get; private set; }

    /// <summary>
    /// Gets the text color code of the tag in hexadecimal format.
    /// </summary>
    public string TextColorCode { get; private set; }

    /// <summary>
    /// Gets the color category group derived from the background color.
    /// </summary>
    public ColorCategory BackgroundColorGroup { get; private set; }

    /// <summary>
    /// Gets the color category group derived from the text color.
    /// </summary>
    public ColorCategory TextColorGroup { get; private set; }
    /// <summary>
    /// 
    /// </summary>
    public IconMetadata? Icon { get; set; }
}
