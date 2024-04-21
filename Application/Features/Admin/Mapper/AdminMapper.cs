using Application.ClientFeatures.Admin.Request;
using AutoMapper;
using Domain.DbEntities;

namespace Application.ClientFeatures.Admin.Mapper;

public sealed class AdminMapper : Profile
{
    public AdminMapper()
    {
        CreateMap<AdminCreateRequest, tblAdmin>().ReverseMap();
        CreateMap<AdminUpdateRequest, tblAdmin>().ReverseMap();
        CreateMap<AdminDeleteRequest, tblAdmin>().ReverseMap();
    }
}