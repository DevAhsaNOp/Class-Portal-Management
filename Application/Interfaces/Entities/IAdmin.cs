using Application.ClientFeatures.Admin.Request;
using Application.ClientFeatures.Admin.Response;
using Application.ClientFeatures.User.Request;
using Application.Features.GenericFeatures;
using Application.Interfaces.Base;
using Domain.DbEntities;

namespace Application.Interfaces.ClientInterfaces
{
    public interface IAdmin : IBaseClientRepository<tblAdmin>
    {
        Task<GenericResponse<dynamic>> Create(AdminCreateRequest request, CancellationToken cancellationToken = default);
        Task<GenericResponse<dynamic>> Update(AdminUpdateRequest request, CancellationToken cancellationToken = default);
        Task<GenericResponse<dynamic>> Delete(AdminDeleteRequest request, CancellationToken cancellationToken = default);
        Task<List<AdminResponse>> GetByStatuses(CancellationToken cancellationToken = default);
        Task<UserAndInstructorDetails> GetNewUsersAndInstructors(CancellationToken cancellationToken = default);
        Task<AdminResponse> GetById(long Id, CancellationToken cancellationToken = default);
        Task<bool> IsEmailExist(string email, CancellationToken cancellationToken = default);
        Task<GenericResponse<dynamic>> CheckLogin(UserLoginRequest request, CancellationToken cancellationToken = default);
    }
}
