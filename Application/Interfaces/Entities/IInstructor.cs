using Application.ClientFeatures.Instructor.Request;
using Application.ClientFeatures.Instructor.Response;
using Application.ClientFeatures.User.Request;
using Application.Features.GenericFeatures;
using Application.Interfaces.Base;
using Domain.DbEntities;

namespace Application.Interfaces.ClientInterfaces
{
    public interface IInstructor : IBaseClientRepository<tblInstructor>
    {
        Task<GenericResponse<dynamic>> Create(InstructorCreateRequest request, CancellationToken cancellationToken = default);
        Task<GenericResponse<dynamic>> Update(InstructorUpdateRequest request, CancellationToken cancellationToken = default);
        Task<GenericResponse<dynamic>> Delete(InstructorDeleteRequest request, CancellationToken cancellationToken = default);
        Task<List<InstructorResponse>> GetByStatuses(CancellationToken cancellationToken = default);
        Task<InstructorResponse> GetById(long Id, CancellationToken cancellationToken = default);
        Task<bool> IsEmailExits(string email, CancellationToken cancellationToken = default);
        Task<GenericResponse<dynamic>> CheckLogin(UserLoginRequest request, CancellationToken cancellationToken = default);
    }
}
