using Application.ClientFeatures.Enrollment.Request;
using Application.Features.GenericFeatures;
using Application.Interfaces.Base;
using Domain.DbEntities;

namespace Application.Interfaces.ClientInterfaces
{
    public interface IEnrollment : IBaseClientRepository<tblEnrollment>
    {
        Task<GenericResponse<dynamic>> Create(EnrollmentCreateRequest request, CancellationToken cancellationToken);
        Task<GenericResponse<dynamic>> Update(EnrollmentUpdateRequest request, CancellationToken cancellationToken);
        Task<GenericResponse<dynamic>> Delete(EnrollmentDeleteRequest request, CancellationToken cancellationToken);
        Task<GenericResponse<dynamic>> GetByStatuses(PaginationRequest pagination);
        Task<GenericResponse<dynamic>> GetById(long Id, CancellationToken cancellationToken);
    }
}
