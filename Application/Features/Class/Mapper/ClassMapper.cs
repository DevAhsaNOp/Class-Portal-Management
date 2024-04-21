using Application.ClientFeatures.Class.Request;
using AutoMapper;
using Domain.DbEntities;

namespace Application.ClientFeatures.Class.Mapper;

public sealed class ClassMapper : Profile
{
    public ClassMapper()
    {
        CreateMap<ClassCreateRequest, tblClass>().ReverseMap();
        CreateMap<ClassUpdateRequest, tblClass>().ReverseMap();
        CreateMap<ClassDeleteRequest, tblClass>().ReverseMap();
    }
}