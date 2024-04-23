using Application.ClientFeatures.Class.Response;
using Application.ClientFeatures.Instructor.Request;
using Application.ClientFeatures.Instructor.Response;
using Application.ClientFeatures.Instructor.Validator;
using Application.ClientFeatures.User.Request;
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
    public class InstructorRepository : BaseClientRepository<tblInstructor>, IInstructor
    {
        private readonly IHelper _helper;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly SystemMessages _systemMessages;
        private readonly DirectoryDetails adminDirectoryDetail = new()
        {
            Name = "Instructor",
            Path = "Instructor"
        };

        public InstructorRepository(DataContext
         dbContext,
         IHelper helper,
         IUnitOfWork unitOfWork,
         IOptions<SystemMessages> SystemMessages,
         IMapper mapper) : base(dbContext)
        {
            _helper = helper;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _systemMessages = SystemMessages.Value;
        }

        public async Task<List<InstructorResponseV2>> GetByStatuses(CancellationToken cancellationToken)
        {
            var list = await FilterIQueryable(x => true)
                .Select(x => new InstructorResponseV2
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    Email = x.Email,
                    Image = string.IsNullOrEmpty(x.Image) ? string.Empty : string.Concat(AppSetting.DocumentUrl, "\\Assets\\", x.Image),
                    Age = x.Age,
                    DateOfJoined = x.DateOfJoined,
                    Gender = x.Gender,
                    Qualification = x.Qualification,
                    Username = x.Username,
                    Status = x.Status,
                    CreatedAt = x.CreatedAt,
                })
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync(cancellationToken);

            return list;
        }

        public async Task<GenericResponse<dynamic>> Create(InstructorCreateRequest request, CancellationToken cancellationToken)
        {
            var validator = new InstructorCreateRequestValidator();
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
            var entity = _mapper.Map<tblInstructor>(request);
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
                    result = "Instructor Details Created Successfully!"
                };
            }
            else
            {
                return new GenericResponse<dynamic>
                {
                    message = _systemMessages.Error,
                    code = HttpStatusCode.BadRequest,
                    result = "An error occurred! Please try again!"
                };
            }
        }

        public async Task<GenericResponse<dynamic>> Update(InstructorUpdateRequest request, CancellationToken cancellationToken)
        {
            var validator = new InstructorUpdateRequestValidator();
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
                    result = "Instructor Details Updated Successfully!"
                };
            }
            else
            {
                return new GenericResponse<dynamic>
                {
                    message = _systemMessages.Error,
                    code = HttpStatusCode.BadRequest,
                    result = "An error occurred! Please try again!"
                };
            }
        }

        public async Task<GenericResponse<dynamic>> Delete(InstructorDeleteRequest request, CancellationToken cancellationToken)
        {
            var validator = new InstructorDeleteRequestValidator();
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
                    result = "Instructor Details Deleted Successfully!"
                };
            }
            else
            {
                return new GenericResponse<dynamic>
                {
                    message = _systemMessages.Error,
                    code = HttpStatusCode.BadRequest,
                    result = "An error occurred! Please try again!"
                };
            }
        }

        public async Task<InstructorResponse> GetById(long Id, CancellationToken cancellationToken)
        {
            var findEntity = await FilterIQueryable(x => x.Id == Id && x.Status == 1)
                .Select(x => new InstructorResponse
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    Email = x.Email,
                    Image = string.IsNullOrEmpty(x.Image) ? string.Empty : string.Concat(AppSetting.DocumentUrl, "\\Assets\\", x.Image),
                    Age = x.Age,
                    DateOfJoined = x.DateOfJoined,
                    Gender = x.Gender,
                    Password = _helper.DecryptFromBase64String(x.Password),
                    Qualification = x.Qualification,
                    Role = "Instructor",
                    Username = x.Username,
                    Status = x.Status,
                    Classes = x.Classes
                    .Select(c => new ClassResponseV2
                    {
                        Id = c.Id,
                        ClassName = c.ClassName,
                        Description = c.Description,
                        AgeGroups = c.AgeGroups,
                        StartTiming = c.StartTiming,
                        EndTiming = c.EndTiming,
                        Fees = c.Fees,
                        GradeLevel = c.GradeLevel,
                        Image = string.IsNullOrEmpty(c.Image) ? string.Empty : string.Concat(AppSetting.DocumentUrl, "\\Assets\\", c.Image),
                        MaxClassSize = c.MaxClassSize,
                        Status = c.Status,
                    }).ToList(),
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
            return await FilterIQueryable(x => x.Email == email && x.Status == 1).AnyAsync(cancellationToken);
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
                .Select(x => new InstructorResponse
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    Email = x.Email,
                    Image = string.IsNullOrEmpty(x.Image) ? string.Empty : string.Concat(AppSetting.DocumentUrl, "\\Assets\\", x.Image),
                    Username = x.Username,
                    Password = x.Password,
                    Age = x.Age,
                    DateOfJoined = x.DateOfJoined,
                    Gender = x.Gender,
                    Qualification = x.Qualification,
                    Role = "Instructor",
                    Status = x.Status,
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy,
                    DeletedAt = x.DeletedAt,
                    DeletedBy = x.DeletedBy,
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (user == null)
            {
                return new GenericResponse<dynamic>
                {
                    message = _systemMessages.DataNotFound,
                    code = HttpStatusCode.BadRequest,
                    result = "Email or Password is incorrect!"
                };
            }

            return new GenericResponse<dynamic>
            {
                message = _systemMessages.Success,
                code = HttpStatusCode.OK,
                result = user
            };
        }

        public async Task<List<InstructorInfo>> GetAllActive(CancellationToken cancellationToken = default)
        {
            var instructors = await FilterIQueryable(x => x.Status == 1)
                .OrderByDescending(i => i.CreatedAt)
                .Select(x => new InstructorInfo
                {
                    Id = x.Id,
                    FullName = x.FullName
                })
                .ToListAsync(cancellationToken);

            return instructors;
        }
    }
}
