using Application.ClientFeatures.Instructor.Request;
using AutoMapper;
using Domain.DbEntities;

namespace Application.ClientFeatures.Instructor.Mapper;

public sealed class InstructorMapper : Profile
{
    public InstructorMapper()
    {
        CreateMap<InstructorCreateRequest, tblInstructor>().ReverseMap();
        CreateMap<InstructorUpdateRequest, tblInstructor>().ReverseMap();
        CreateMap<InstructorDeleteRequest, tblInstructor>().ReverseMap();
    }
}