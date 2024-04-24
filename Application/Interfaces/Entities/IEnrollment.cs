using Application.ClientFeatures.Enrollment.Request;
using Application.ClientFeatures.Enrollment.Response;
using Application.Features.GenericFeatures;
using Application.Interfaces.Base;
using Domain.DbEntities;

namespace Application.Interfaces.ClientInterfaces
{
    public interface IEnrollment : IBaseClientRepository<tblEnrollment>
    {
        Task<GenericResponse<dynamic>> Create(EnrollmentCreateRequest request, CancellationToken cancellationToken = default);
        Task<GenericResponse<dynamic>> Update(EnrollmentUpdateRequest request, CancellationToken cancellationToken = default);
        Task<GenericResponse<dynamic>> Delete(EnrollmentDeleteRequest request, CancellationToken cancellationToken = default);
        Task<List<EnrollmentResponse>> GetByStatuses(CancellationToken cancellationToken = default);
        Task<EnrollmentResponse> GetById(long Id, CancellationToken cancellationToken = default);
        Task<EnrollmentResponseV2> GetAllEnrolledUserByClassId(int ClassId, CancellationToken cancellationToken = default);
    }
}
