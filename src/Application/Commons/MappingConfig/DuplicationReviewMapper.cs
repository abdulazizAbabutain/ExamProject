using Application.Commons.SharedModelResult;
using Domain.Auditing;
using Mapster;

namespace Application.Commons.MappingConfig
{
    public class DuplicationReviewMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<DuplicationReviewMetadata, DuplicationReviewResult>();
        }
    }
}
