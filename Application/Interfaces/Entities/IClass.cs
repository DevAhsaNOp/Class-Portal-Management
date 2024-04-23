using Application.ClientFeatures.Class.Request;
using Application.ClientFeatures.Class.Response;
using Application.Features.GenericFeatures;
using Application.Interfaces.Base;
using Domain.DbEntities;

namespace Application.Interfaces.ClientInterfaces
{
    public interface IClass : IBaseClientRepository<tblClass>
    {
        Task<GenericResponse<dynamic>> Create(ClassCreateRequest request, CancellationToken cancellationToken = default);
        Task<GenericResponse<dynamic>> Update(ClassUpdateRequest request, CancellationToken cancellationToken = default);
        Task<GenericResponse<dynamic>> Delete(ClassDeleteRequest request, CancellationToken cancellationToken = default);
        Task<List<ClassResponse>> GetByStatuses(CancellationToken cancellationToken = default);
        Task<ClassResponse> GetById(long Id, CancellationToken cancellationToken = default);
    }
}
