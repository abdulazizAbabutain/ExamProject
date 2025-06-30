namespace Application.Commons.Models.ServicesModel.Source;

public class AddSourceReferenceServiceModel
{
    public string Note { get; set; }
    public List<AddMetadataSourceModel>? Metadata { get; set; }
}
