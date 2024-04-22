using Application.ClientFeatures.Admin.Response;
using Application.ClientFeatures.User.Request;
using Application.ClientFeatures.User.Response;
using Application.ClientFeatures.User.Validator;
using Application.Common.Behaviors;
using Application.Features.GenericFeatures;
using Application.Interfaces;
using Application.Interfaces.Base;
using Application.Interfaces.ClientInterfaces;
using AutoMapper;
using Domain.DbEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Persistence.Authentication;
using Persistence.Context;
using Persistence.Repositories.Common;
using System.Net;

namespace Persistence.Repositories.ClientRepositories
{
    public class UserRepository : BaseClientRepository<tblUser>, IUser
    {
        private readonly IHelper _helper;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly SystemMessages _systemMessages;
        private readonly IAdmin _admin;
        private readonly IInstructor _instructor;

        private readonly DirectoryDetails adminDirectoryDetail = new()
        {
            Name = "User",
            Path = "User"
        };

        public UserRepository(DataContext
         dbContext,
         IHelper helper,
         IUnitOfWork unitOfWork,
         IOptions<SystemMessages> SystemMessages,
         IMapper mapper,
         IAdmin admin,
         IInstructor instructor) : base(dbContext)
        {
            _helper = helper;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _systemMessages = SystemMessages.Value;
            _admin = admin;
            _instructor = instructor;
        }

        public async Task<List<UserResponseV2>> GetByStatuses(CancellationToken cancellationToken)
        {
            var list = await FilterIQueryable(x => true)
                .Select(x => new UserResponseV2
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    Email = x.Email,
                    Image = string.IsNullOrEmpty(x.Image) ? string.Empty : string.Concat(AppSetting.DocumentUrl, "\\Assets\\", x.Image),
                    Username = x.Username,
                    Status = x.Status,
                    Role = "User",
                    CreatedAt = x.CreatedAt,
                })
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync(cancellationToken);

            return list;
        }

        public async Task<GenericResponse<dynamic>> Create(UserCreateRequest request, CancellationToken cancellationToken)
        {
            var validator = new UserCreateRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new GenericResponse<dynamic>
                {
                    message = _systemMessages.ValidationError,
                    code = HttpStatusCode.Forbidden,
                    result = validationResult.Errors.Select(x => new { x.PropertyName, x.ErrorMessage }).ToList()
                };
            }

            var imagePath = _helper.AddInAttachmentStore(request.Image, adminDirectoryDetail);
            var entity = _mapper.Map<tblUser>(request);
            entity.Password = _helper.Encryptor(request.Password);
            entity.Image = imagePath;
            Create(entity);
            var result = await _unitOfWork.Save(cancellationToken);

            if (result > 0)
            {
                return new GenericResponse<dynamic>
                {
                    message = _systemMessages.Success,
                    code = HttpStatusCode.OK,
                    result = "User Details Created Successfully!"
                };
            }
            else
            {
                return new GenericResponse<dynamic>
                {
                    message = _systemMessages.Error,
                    code = HttpStatusCode.BadRequest,
                    result = "An error occurred! Please try again later."
                };
            }
        }

        public async Task<GenericResponse<dynamic>> Update(UserUpdateRequest request, CancellationToken cancellationToken)
        {
            var validator = new UserUpdateRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new GenericResponse<dynamic>
                {
                    message = _systemMessages.ValidationError,
                    code = HttpStatusCode.Forbidden,
                    result = validationResult.Errors.Select(x => new { x.PropertyName, x.ErrorMessage }).ToList()
                };
            }

            var findEntity = await Get(request.Id, cancellationToken);

            if (findEntity == null)
            {
                return new GenericResponse<dynamic>
                {
                    message = _systemMessages.DataNotFound,
                    code = HttpStatusCode.BadRequest
                };
            }

            request.Password = string.IsNullOrEmpty(request.Password) ? findEntity.Password : _helper.Encryptor(request.Password);

            _mapper.Map(request, findEntity);

            if (request.Image is null && !string.IsNullOrEmpty(request.ProfileImage))
            {
                request.ProfileImage = string.IsNullOrEmpty(request.ProfileImage) ? string.Empty : request.ProfileImage.Split("Assets\\")[1];
                findEntity.Image = request.ProfileImage;
            }
            else
                findEntity.Image = _helper.AddInAttachmentStore(request.Image, adminDirectoryDetail);

            Update(findEntity);
            var result = await _unitOfWork.Save(cancellationToken);

            if (result > 0)
            {
                return new GenericResponse<dynamic>
                {
                    message = _systemMessages.Success,
                    code = HttpStatusCode.OK,
                    result = "Account Updated Successfully!"
                };
            }
            else
            {
                return new GenericResponse<dynamic>
                {
                    message = _systemMessages.Error,
                    code = HttpStatusCode.BadRequest
                };
            }
        }

        public async Task<GenericResponse<dynamic>> Delete(UserDeleteRequest request, CancellationToken cancellationToken)
        {
            var validator = new UserDeleteRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new GenericResponse<dynamic>
                {
                    message = _systemMessages.ValidationError,
                    code = HttpStatusCode.Forbidden,
                    result = validationResult.Errors.Select(x => new { x.PropertyName, x.ErrorMessage }).ToList()
                };
            }

            var findEntity = await Get(request.Id, cancellationToken);

            if (findEntity == null)
            {
                return new GenericResponse<dynamic>
                {
                    message = _systemMessages.DataNotFound,
                    code = HttpStatusCode.BadRequest
                };
            }

            _mapper.Map(request, findEntity);
            Delete(findEntity);
            var result = await _unitOfWork.Save(cancellationToken);

            if (result > 0)
            {
                return new GenericResponse<dynamic>
                {
                    message = _systemMessages.Success,
                    code = HttpStatusCode.OK,
                    result = "User Deleted Successfully!"
                };
            }
            else
            {
                return new GenericResponse<dynamic>
                {
                    message = _systemMessages.Error,
                    code = HttpStatusCode.BadRequest
                };
            }
        }

        public async Task<UserResponse> GetById(long Id, CancellationToken cancellationToken)
        {
            var findEntity = await FilterIQueryable(x => x.Id == Id && x.Status == 1)
                .Select(x => new UserResponse
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    Email = x.Email,
                    Image = string.IsNullOrEmpty(x.Image) ? string.Empty : string.Concat(AppSetting.DocumentUrl, "\\Assets\\", x.Image),
                    Username = x.Username,
                    Password = _helper.DecryptFromBase64String(x.Password),
                    Status = x.Status,
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy,
                    DeletedAt = x.DeletedAt,
                    DeletedBy = x.DeletedBy,
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (findEntity == null)
            {
                return null;
            }

            return findEntity;
        }

        public async Task<bool> IsEmailExits(string email, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            var checkUser = await FilterIQueryable(x => x.Email == email && x.Status == 1).AnyAsync(cancellationToken);

            if (checkUser)
                return true;

            var checkInstructor = await _instructor.IsEmailExits(email, cancellationToken);

            if (checkInstructor)
                return true;

            var checkAdmin = await _admin.IsEmailExist(email, cancellationToken);

            if (checkAdmin)
                return true;

            return false;
        }

        public async Task<GenericResponse<dynamic>> CheckLogin(UserLoginRequest request, CancellationToken cancellationToken = default)
        {
            var validator = new UserLoginRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new GenericResponse<dynamic>
                {
                    message = _systemMessages.ValidationError,
                    code = HttpStatusCode.Forbidden,
                    result = validationResult.Errors.Select(x => new { x.PropertyName, x.ErrorMessage }).ToList()
                };
            }

            var user = await FilterIQueryable(x => x.Email == request.Email
                && x.Password == _helper.Encryptor(request.Password) && x.Status == 1)
                .Select(x => new UserResponse
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    Email = x.Email,
                    Image = string.IsNullOrEmpty(x.Image) ? string.Empty : string.Concat(AppSetting.DocumentUrl, "\\Assets\\", x.Image),
                    Username = x.Username,
                    Password = x.Password,
                    Role = "User",
                    Status = x.Status,
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy,
                    DeletedAt = x.DeletedAt,
                    DeletedBy = x.DeletedBy,
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (user is not null)
            {
                return new GenericResponse<dynamic>
                {
                    message = _systemMessages.Success,
                    code = HttpStatusCode.OK,
                    result = user
                };
            }

            var instructor = await _instructor.CheckLogin(request, cancellationToken);

            if (instructor is not null &&
                instructor.code == HttpStatusCode.OK)
                return instructor;

            var admin = await _admin.CheckLogin(request, cancellationToken);

            if (admin is not null &&
                admin.code == HttpStatusCode.OK)
                return admin;

            return new GenericResponse<dynamic>
            {
                message = _systemMessages.Error,
                code = HttpStatusCode.BadRequest,
                result = "Email or Password is incorrect!"
            };
        }

        public async Task<GenericResponse<dynamic>> Update(UserUpdateView request, CancellationToken cancellationToken = default)
        {
            if (request is null)
            {
                return new GenericResponse<dynamic>
                {
                    message = _systemMessages.ValidationError,
                    code = HttpStatusCode.Forbidden,
                    result = "Request is null!"
                };
            }

            var findEntity = _mapper.Map<UserUpdateRequest>(request);
            return await Update(findEntity, cancellationToken);
        }

        public async Task<GenericResponse<dynamic>> ChangePassword(UserChangePasswordRequest request, CancellationToken cancellationToken = default)
        {
            var validator = new UserChangePasswordRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new GenericResponse<dynamic>
                {
                    message = _systemMessages.ValidationError,
                    code = HttpStatusCode.Forbidden,
                    result = validationResult.Errors.Select(x => new { x.PropertyName, x.ErrorMessage }).ToList()
                };
            }

            var findEntity = await Get(request.Id, cancellationToken);

            if (findEntity == null)
            {
                return new GenericResponse<dynamic>
                {
                    message = _systemMessages.DataNotFound,
                    code = HttpStatusCode.BadRequest,
                    result = "User not found!"
                };
            }

            var isOldPasswordMatch = _helper.Encryptor(request.OldPassword) == findEntity.Password;

            if (!isOldPasswordMatch)
            {
                return new GenericResponse<dynamic>
                {
                    message = _systemMessages.Error,
                    code = HttpStatusCode.BadRequest,
                    result = "Old Password is incorrect!"
                };
            }

            findEntity.Password = _helper.Encryptor(request.Password);
            findEntity.UpdatedBy = request.UpdatedBy;
            findEntity.Status = request.Status;
            Update(findEntity);
            var result = await _unitOfWork.Save(cancellationToken);

            if (result > 0)
            {
                return new GenericResponse<dynamic>
                {
                    message = _systemMessages.Success,
                    code = HttpStatusCode.OK,
                    result = "Password Changed Successfully! Please login again."
                };
            }
            else
            {
                return new GenericResponse<dynamic>
                {
                    message = _systemMessages.Error,
                    code = HttpStatusCode.BadRequest,
                    result = "An error occurred! Please try again later."
                };
            }
        }

        public async Task<List<UserResponseV2>> GetNonActiveUsers(CancellationToken cancellationToken = default)
        {
            var nonActiveUsers = await FilterIQueryable(x => x.Status == 2)
                .Select(x => new UserResponseV2
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    Email = x.Email,
                    Image = string.IsNullOrEmpty(x.Image) ? string.Empty : string.Concat(AppSetting.DocumentUrl, "\\Assets\\", x.Image),
                    Username = x.Username,
                    Role = "User",
                    Status = x.Status,
                    CreatedAt = x.CreatedAt,
                })
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync(cancellationToken);

            return nonActiveUsers;
        }

        public async Task<UserAndInstructorDetails> GetNewUsersAndInstructors(CancellationToken cancellationToken = default)
        {
            var usersList = await GetByStatuses(cancellationToken);

            var instructorsList = await _instructor.GetByStatuses(cancellationToken);

            var nonActiveUsersList = await GetNonActiveUsers(cancellationToken);

            return new UserAndInstructorDetails
            {
                Users = usersList,
                NonActiveUsers = nonActiveUsersList,
                Instructors = instructorsList
            };
        }

        public async Task<UserResponseV2> GetUserDetailId(long Id, CancellationToken cancellationToken = default)
        {
            var findEntity = await FilterIQueryable(x => x.Id == Id && x.Status == 2)
                .Select(x => new UserResponseV2
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    Email = x.Email,
                    Image = string.IsNullOrEmpty(x.Image) ? string.Empty : string.Concat(AppSetting.DocumentUrl, "\\Assets\\", x.Image),
                    Username = x.Username,
                    Role = "User",
                    Status = x.Status,
                    CreatedAt = x.CreatedAt,
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (findEntity == null)
            {
                return null;
            }

            return findEntity;
        }

        public async Task<GenericResponse<dynamic>> ChangeUserStatus(UserStatusChangeRequest user, CancellationToken cancellationToken = default)
        {
            var findEntity = await Get(user.Id, cancellationToken);

            if (findEntity == null)
            {
                return new GenericResponse<dynamic>
                {
                    message = _systemMessages.DataNotFound,
                    code = HttpStatusCode.BadRequest
                };
            }

            findEntity.Status = user.Status;
            findEntity.UpdatedBy = user.UpdatedBy;
            Update(findEntity);
            var result = await _unitOfWork.Save(cancellationToken);

            if (result > 0)
            {
                return new GenericResponse<dynamic>
                {
                    message = _systemMessages.Success,
                    code = HttpStatusCode.OK,
                    result = "User Active Successfully!"
                };
            }
            else
            {
                return new GenericResponse<dynamic>
                {
                    message = _systemMessages.Error,
                    code = HttpStatusCode.BadRequest,
                    result = "An error occurred! Please try again later."
                };
            }
        }
    }
}
