using Application.Commons.SharedModelResult.Icons;
using Domain.Entities.Metadata;
using Mapster;

namespace Application.Commons.MappingConfig;

internal class IconMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<IconMetadata, IconMetadataResult>();
    }
}
