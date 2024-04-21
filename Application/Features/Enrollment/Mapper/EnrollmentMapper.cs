using Application.ClientFeatures.Enrollment.Request;
using AutoMapper;
using Domain.DbEntities;

namespace Application.ClientFeatures.Enrollment.Mapper;

public sealed class EnrollmentMapper : Profile
{
    public EnrollmentMapper()
    {
        CreateMap<EnrollmentCreateRequest, tblEnrollment>().ReverseMap();
        CreateMap<EnrollmentUpdateRequest, tblEnrollment>().ReverseMap();
        CreateMap<EnrollmentDeleteRequest, tblEnrollment>().ReverseMap();
    }
}