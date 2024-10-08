﻿using KDrom.Application.MediatR.Makes.Dtos;
using KDrom.Domain.Entities;
using Mapster;

namespace KDrom.Application.Common.Mappings;

public class MappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Make, MakeListDto>()
            .Map(dest => dest.MakeNames, src => src.Name);
    }
}
