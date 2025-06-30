using Domain.Enums;

namespace Application.Commons.Models.ServicesModel.Source;

public class AddMetadataSourceModel
{
    public string FiledName { get; set; }
    public string Value { get; set; }
    public FiledType FiledType { get; set; }
}
