using KDrom.Application.Makes.Queries.GetList;
using KDrom.Domain.Entities;
using Mapster;

namespace KDrom.Application.Common.Mappings;

public class MappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Make, MakeListVm>()
            .Map(dest => dest.MakeNames, src => src.Name);
    }
}
