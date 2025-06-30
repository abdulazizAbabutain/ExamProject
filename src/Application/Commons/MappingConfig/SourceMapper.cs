using Application.Commons.Models.ServicesModel.Source;
using Application.Commons.SharedModelResult.Source;
using Application.Sources.Commands.AddSource.Requests;
using Application.Sources.Commands.AddSource.Results;
using Domain.Entities.Sources;
using Mapster;

namespace Application.Commons.MappingConfig
{
    public class SourceMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AddSourceReferenceCommand, AddSourceReferenceServiceModel>();
            config.NewConfig<AddSourceCommand, AddSourceServiceModel>();




            config.NewConfig<Source,AddSourceCommandResult>();
            config.NewConfig<SourceReference, AddSourceReferenceCommand>();
            config.NewConfig<Metadata, MetadataResult>();







            config.NewConfig<AddMetadataSourceModel, Metadata>().MapWith(src => new Metadata(src.FiledName, src.Value, src.FiledType));
            

        }
    }
}
