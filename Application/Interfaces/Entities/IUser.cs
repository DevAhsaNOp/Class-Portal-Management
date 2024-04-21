using Application.ClientFeatures.User.Request;
using Application.ClientFeatures.User.Response;
using Application.Features.GenericFeatures;
using Application.Interfaces.Base;
using Domain.DbEntities;

namespace Application.Interfaces.ClientInterfaces
{
    public interface IUser : IBaseClientRepository<tblUser>
    {
        Task<GenericResponse<dynamic>> Create(UserCreateRequest request, CancellationToken cancellationToken = default);
        Task<GenericResponse<dynamic>> Update(UserUpdateRequest request, CancellationToken cancellationToken = default);
        Task<GenericResponse<dynamic>> Update(UserUpdateView request, CancellationToken cancellationToken = default);
        Task<GenericResponse<dynamic>> ChangePassword(UserChangePasswordRequest request, CancellationToken cancellationToken = default);
        Task<GenericResponse<dynamic>> Delete(UserDeleteRequest request, CancellationToken cancellationToken = default);
        Task<List<UserResponse>> GetByStatuses(CancellationToken cancellationToken = default);
        Task<UserResponse> GetById(long Id, CancellationToken cancellationToken = default);
        Task<bool> IsEmailExits(string email, CancellationToken cancellationToken = default);
        Task<GenericResponse<dynamic>> CheckLogin(UserLoginRequest request, CancellationToken cancellationToken = default);
    }
}
