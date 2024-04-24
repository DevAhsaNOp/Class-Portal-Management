using Application.ClientFeatures.Enrollment.Request;
using Application.ClientFeatures.Enrollment.Response;
using Application.ClientFeatures.Enrollment.Validator;
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
    public class EnrollmentRepository : BaseClientRepository<tblEnrollment>, IEnrollment
    {
        private readonly IHelper _helper;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly SystemMessages _systemMessages;

        public EnrollmentRepository(DataContext
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

        public async Task<List<EnrollmentResponse>> GetByStatuses(CancellationToken cancellationToken)
        {
            var list = await FilterIQueryable(x => true)
                .Select(x => new EnrollmentResponse
                {
                    Id = x.Id,
                    ClassID = x.ClassID,
                    ClassName = x.Class.ClassName,
                    GradeLevel = x.Class.GradeLevel,
                    UserID = x.UserID,
                    UserName = x.User.FullName,
                    UserImage = string.IsNullOrEmpty(x.User.Image) ? string.Empty : string.Concat(AppSetting.DocumentUrl, "\\Assets\\", x.User.Image),
                    InstructorID = x.Class.InstructorID,
                    InstructorName = x.Class.Instructor.FullName,
                    EnrollmentDate = x.EnrollmentDate,
                    Status = x.Status,
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy,
                    DeletedAt = x.DeletedAt,
                    DeletedBy = x.DeletedBy,
                })
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync(cancellationToken);

            return list;
        }

        public async Task<GenericResponse<dynamic>> Create(EnrollmentCreateRequest request, CancellationToken cancellationToken)
        {
            var validator = new EnrollmentCreateRequestValidator();
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

            var entity = _mapper.Map<tblEnrollment>(request);
            Create(entity);
            var result = await _unitOfWork.Save(cancellationToken);

            if (result > 0)
            {
                return new GenericResponse<dynamic>
                {
                    message = _systemMessages.Success,
                    code = HttpStatusCode.OK,
                    result = "User has been Enrolled Successfully!"
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

        public async Task<GenericResponse<dynamic>> Update(EnrollmentUpdateRequest request, CancellationToken cancellationToken)
        {
            var validator = new EnrollmentUpdateRequestValidator();
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
            Update(findEntity);
            var result = await _unitOfWork.Save(cancellationToken);

            if (result > 0)
            {
                return new GenericResponse<dynamic>
                {
                    message = _systemMessages.Success,
                    code = HttpStatusCode.OK,
                    result = "User Enrollment Details Updated Successfully!"
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

        public async Task<GenericResponse<dynamic>> Delete(EnrollmentDeleteRequest request, CancellationToken cancellationToken)
        {
            var validator = new EnrollmentDeleteRequestValidator();
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
                    result = "Enrollment Details Deleted Successfully!"
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

        public async Task<EnrollmentResponse> GetById(long Id, CancellationToken cancellationToken)
        {
            var findEntity = await FilterIQueryable(x => x.Id == Id && x.Status == 1)
                .Select(x => new EnrollmentResponse
                {
                    Id = x.Id,
                    ClassID = x.ClassID,
                    ClassName = x.Class.ClassName,
                    UserImage = string.IsNullOrEmpty(x.User.Image) ? string.Empty : string.Concat(AppSetting.DocumentUrl, "\\Assets\\", x.User.Image),
                    GradeLevel = x.Class.GradeLevel,
                    UserID = x.UserID,
                    UserName = x.User.FullName,
                    InstructorID = x.Class.InstructorID,
                    InstructorName = x.Class.Instructor.FullName,
                    EnrollmentDate = x.EnrollmentDate,
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
    }
}
