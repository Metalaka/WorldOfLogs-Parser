using System;
using System.Globalization;
using AutoMapper;

namespace FileImporter
{
    public class AutoMapperProfile : Profile
    {
        // mappings between model and entity objects
        public AutoMapperProfile()
        {
            CreateMap<Domain.Wol.Spell, Domain.Entities.Spell>()
                .ForMember(_ => _.Schools, expression => expression.Ignore())
                //todo: Schools from School/Schools
                ;
            CreateMap<Domain.Wol.Guild, Domain.Entities.Guild>()
                .ForMember(_ => _.Id, expression => expression.MapFrom(dto => dto.gid))
                ;
            CreateMap<Domain.Wol.Report, Domain.Entities.Report>()
                .ForMember(_ => _.TimeInfoE, expression => expression.MapFrom(dto => dto.timeInfo.e))
                .ForMember(_ => _.TimeInfoS, expression => expression.MapFrom(dto => dto.timeInfo.s))
                ;
            CreateMap<Domain.Wol.Actor, Domain.Entities.Actor>()
                .ForMember(_ => _.Guid, expression => expression.MapFrom(dto => Int64.Parse(dto.GUIDString.Replace("x", ""), NumberStyles.HexNumber)))
                ;
            CreateMap<Domain.Wol.FlaggedActor, Domain.Entities.FlaggedActor>()
                ;
            CreateMap<Domain.Wol.Event, Domain.Entities.Event>()
                .ForMember(_ => _.Spell, expression => expression.Ignore())
                .ForMember(_ => _.ExtraSpell, expression => expression.Ignore())
                //todo: Spell
                ;
        }
    }
}