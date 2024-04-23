using Application.ClientFeatures.Class.Request;
using FluentValidation;
using static Application.Common.Behaviors.Statuses;

namespace Application.ClientFeatures.Class.Validator;

public sealed class ClassCreateRequestValidator : AbstractValidator<ClassCreateRequest>
{
    public ClassCreateRequestValidator()
    {
        RuleFor(x => x.ClassName).NotNull().NotEmpty().MaximumLength(100);
        RuleFor(x => x.GradeLevel).NotNull().NotEmpty().NotEqual(0);
        RuleFor(x => x.Image).NotNull().NotEmpty();
        RuleFor(x => x.Description).NotNull().NotEmpty();
        RuleFor(x => x.AgeGroups).NotNull().NotEmpty().MaximumLength(150);
        RuleFor(x => x.Fees).NotNull().NotEmpty().NotEqual(0);
        RuleFor(x => x.StartTiming).NotNull().NotEmpty();
        RuleFor(x => x.EndTiming).NotNull().NotEmpty();
        RuleFor(x => x.MaxClassSize).NotNull().NotEmpty().NotEqual(0);
        RuleFor(x => x.InstructorID).NotNull().NotEmpty().NotEqual(0);
        RuleFor(x => x.CreatedBy).NotEmpty().NotEqual(0);
        RuleFor(x => x.Status).NotEmpty().NotEqual(0).LessThanOrEqualTo(SystemStatus.InActive.GetHashCode());
    }
}

public sealed class ClassUpdateRequestValidator : AbstractValidator<ClassUpdateRequest>
{
    public ClassUpdateRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotEqual(0);
        RuleFor(x => x.ClassName).NotNull().NotEmpty().MaximumLength(100);
        RuleFor(x => x.GradeLevel).NotNull().NotEmpty().NotEqual(0);
        RuleFor(x => x.Description).NotNull().NotEmpty();
        RuleFor(x => x.AgeGroups).NotNull().NotEmpty().MaximumLength(150);
        RuleFor(x => x.Fees).NotNull().NotEmpty().NotEqual(0);
        RuleFor(x => x.StartTiming).NotNull().NotEmpty();
        RuleFor(x => x.EndTiming).NotNull().NotEmpty();
        RuleFor(x => x.MaxClassSize).NotNull().NotEmpty().NotEqual(0);
        RuleFor(x => x.InstructorID).NotNull().NotEmpty().NotEqual(0);
        RuleFor(x => x.UpdatedBy).NotEmpty().NotEqual(0);
        RuleFor(x => x.Status).NotEmpty().NotEqual(0).LessThanOrEqualTo(SystemStatus.InActive.GetHashCode());
    }
}

public sealed class ClassDeleteRequestValidator : AbstractValidator<ClassDeleteRequest>
{
    public ClassDeleteRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotEqual(0);
        RuleFor(x => x.DeletedBy).NotEmpty();
        RuleFor(x => x.Status).NotEmpty().Equal(SystemStatus.Deleted.GetHashCode());
    }
}
