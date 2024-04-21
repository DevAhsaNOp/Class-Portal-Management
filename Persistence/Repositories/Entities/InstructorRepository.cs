﻿using Application.ClientFeatures.Instructor.Request;
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

        public async Task<List<InstructorResponse>> GetByStatuses(CancellationToken cancellationToken)
        {
            var list = await FilterIQueryable(x => true)
                .Select(x => new InstructorResponse
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    Email = x.Email,
                    Image = string.IsNullOrEmpty(x.Image) ? string.Empty : string.Concat(AppSetting.DocumentUrl, "\\Assets\\", x.Image),
                    Age = x.Age,
                    DateOfJoined = x.DateOfJoined,
                    Gender = x.Gender,
                    Password = x.Password,
                    Qualification = x.Qualification,
                    Username = x.Username,
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
                    code = HttpStatusCode.BadRequest
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

            _mapper.Map(request, findEntity);
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
                    code = HttpStatusCode.BadRequest
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
                    Password = x.Password,
                    Qualification = x.Qualification,
                    Username = x.Username,
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
    }
}
