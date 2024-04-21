using Application.ClientFeatures.User.Request;
using AutoMapper;
using Domain.DbEntities;

namespace Application.ClientFeatures.User.Mapper;

public sealed class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<UserCreateRequest, tblUser>().ReverseMap();
        CreateMap<UserUpdateRequest, tblUser>().ReverseMap();
        CreateMap<UserUpdateView, UserUpdateRequest>().ReverseMap();
        CreateMap<UserDeleteRequest, tblUser>().ReverseMap();
    }
}