using Application.ClientFeatures.Class.Request;
using Application.ClientFeatures.Class.Response;
using Application.ClientFeatures.Class.Validator;
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
    public class ClassRepository : BaseClientRepository<tblClass>, IClass
    {
        private readonly IHelper _helper;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly SystemMessages _systemMessages;

        private readonly DirectoryDetails adminDirectoryDetail = new()
        {
            Name = "Class",
            Path = "Class"
        };

        public ClassRepository(DataContext
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

        public async Task<List<ClassResponse>> GetByStatuses(CancellationToken cancellationToken)
        {
            var list = await FilterIQueryable(x => true)
                .Select(x => new ClassResponse
                {
                    Id = x.Id,
                    AgeGroups = x.AgeGroups,
                    ClassName = x.ClassName,
                    GradeLevel = x.GradeLevel,
                    Description = x.Description,
                    StartTiming = x.StartTiming,
                    EndTiming = x.EndTiming,
                    InstructorID = x.InstructorID,
                    InstructorName = x.Instructor.FullName,
                    Fees = x.Fees,
                    MaxClassSize = x.MaxClassSize,
                    Image = string.IsNullOrEmpty(x.Image) ? string.Empty : string.Concat(AppSetting.DocumentUrl, "\\Assets\\", x.Image),
                    Status = x.Status,
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy,
                    DeletedAt = x.DeletedAt,
                    DeletedBy = x.DeletedBy,
                })
                .ToListAsync(cancellationToken);

            return list;
        }

        public async Task<GenericResponse<dynamic>> Create(ClassCreateRequest request, CancellationToken cancellationToken)
        {
            var validator = new ClassCreateRequestValidator();
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
            var entity = _mapper.Map<tblClass>(request);
            entity.Image = imagePath;
            Create(entity);
            var result = await _unitOfWork.Save(cancellationToken);

            if (result > 0)
            {
                return new GenericResponse<dynamic>
                {
                    message = _systemMessages.Success,
                    code = HttpStatusCode.OK,
                    result = "Class Details Created Successfully!"
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

        public async Task<GenericResponse<dynamic>> Update(ClassUpdateRequest request, CancellationToken cancellationToken)
        {
            var validator = new ClassUpdateRequestValidator();
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
                    result = "Class Details Updated Successfully!"
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

        public async Task<GenericResponse<dynamic>> Delete(ClassDeleteRequest request, CancellationToken cancellationToken)
        {
            var validator = new ClassDeleteRequestValidator();
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
                    code = HttpStatusCode.OK
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

        public async Task<ClassResponse> GetById(long Id, CancellationToken cancellationToken)
        {
            var findEntity = await FilterIQueryable(x => x.Id == Id && x.Status == 1)
                .Select(x => new ClassResponse
                {
                    Id = x.Id,
                    AgeGroups = x.AgeGroups,
                    ClassName = x.ClassName,
                    GradeLevel = x.GradeLevel,
                    Description = x.Description,
                    StartTiming = x.StartTiming,
                    EndTiming = x.EndTiming,
                    InstructorID = x.InstructorID,
                    InstructorName = x.Instructor.FullName,
                    Fees = x.Fees,
                    MaxClassSize = x.MaxClassSize,
                    Image = string.IsNullOrEmpty(x.Image) ? string.Empty : string.Concat(AppSetting.DocumentUrl, "\\Assets\\", x.Image),
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
