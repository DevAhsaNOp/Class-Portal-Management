using Application.ClientFeatures.Instructor.Request;
using FluentValidation;
using static Application.Common.Behaviors.Statuses;

namespace Application.ClientFeatures.Instructor.Validator;

public sealed class InstructorCreateRequestValidator : AbstractValidator<InstructorCreateRequest>
{
    public InstructorCreateRequestValidator()
    {
        RuleFor(x => x.FullName).NotNull().NotEmpty().MaximumLength(150);
        RuleFor(x => x.Gender).NotNull().NotEmpty().MaximumLength(30);
        RuleFor(x => x.Age).NotEmpty().NotEqual(0);
        RuleFor(x => x.DateOfJoined).NotEmpty();
        RuleFor(x => x.Qualification).NotNull().NotEmpty().MaximumLength(150);
        RuleFor(x => x.Image).NotNull();
        RuleFor(x => x.Username).NotNull().NotEmpty().MaximumLength(50);
        RuleFor(x => x.Email).NotNull().NotEmpty().MaximumLength(50);
        RuleFor(x => x.Password).NotNull().NotEmpty().MaximumLength(50);
        RuleFor(x => x.CreatedBy).NotEmpty().NotEqual(0);
        RuleFor(x => x.Status).NotEmpty().NotEqual(0).LessThanOrEqualTo(SystemStatus.InActive.GetHashCode());
    }
}

public sealed class InstructorUpdateRequestValidator : AbstractValidator<InstructorUpdateRequest>
{
    public InstructorUpdateRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotEqual(0);
        RuleFor(x => x.FullName).NotNull().NotEmpty().MaximumLength(150);
        RuleFor(x => x.Gender).NotNull().NotEmpty().MaximumLength(30);
        RuleFor(x => x.Age).NotEmpty().NotEqual(0);
        RuleFor(x => x.DateOfJoined).NotEmpty();
        RuleFor(x => x.Qualification).NotNull().NotEmpty().MaximumLength(150);
        RuleFor(x => x.Image).NotNull();
        RuleFor(x => x.Username).NotNull().NotEmpty().MaximumLength(50);
        RuleFor(x => x.Email).NotNull().NotEmpty().MaximumLength(50);
        RuleFor(x => x.Password).NotNull().NotEmpty().MaximumLength(50);
        RuleFor(x => x.UpdatedBy).NotEmpty().NotEqual(0);
        RuleFor(x => x.Status).NotEmpty().NotEqual(0).LessThanOrEqualTo(SystemStatus.InActive.GetHashCode());
    }
}

public sealed class InstructorDeleteRequestValidator : AbstractValidator<InstructorDeleteRequest>
{
    public InstructorDeleteRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotEqual(0);
        RuleFor(x => x.DeletedBy).NotEmpty();
        RuleFor(x => x.Status).NotEmpty().Equal(SystemStatus.Deleted.GetHashCode());
    }
}
