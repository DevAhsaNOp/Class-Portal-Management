using Application.ClientFeatures.Enrollment.Request;
using FluentValidation;
using static Application.Common.Behaviors.Statuses;

namespace Application.ClientFeatures.Enrollment.Validator;

public sealed class EnrollmentCreateRequestValidator : AbstractValidator<EnrollmentCreateRequest>
{
    public EnrollmentCreateRequestValidator()
    {
        RuleFor(x => x.UserID).NotNull().NotEmpty().NotEqual(0);
        RuleFor(x => x.ClassID).NotNull().NotEmpty().NotEqual(0);
        RuleFor(x => x.EnrollmentDate).NotNull().NotEmpty();
        RuleFor(x => x.CreatedBy).NotEmpty().NotEqual(0);
        RuleFor(x => x.Status).NotEmpty().NotEqual(0).LessThanOrEqualTo(SystemStatus.InActive.GetHashCode());
    }
}

public sealed class EnrollmentUpdateRequestValidator : AbstractValidator<EnrollmentUpdateRequest>
{
    public EnrollmentUpdateRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotEqual(0);
        RuleFor(x => x.UserID).NotNull().NotEmpty().NotEqual(0);
        RuleFor(x => x.ClassID).NotNull().NotEmpty().NotEqual(0);
        RuleFor(x => x.EnrollmentDate).NotNull().NotEmpty();
        RuleFor(x => x.UpdatedBy).NotEmpty().NotEqual(0);
        RuleFor(x => x.Status).NotEmpty().NotEqual(0).LessThanOrEqualTo(SystemStatus.InActive.GetHashCode());
    }
}

public sealed class EnrollmentDeleteRequestValidator : AbstractValidator<EnrollmentDeleteRequest>
{
    public EnrollmentDeleteRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotEqual(0);
        RuleFor(x => x.DeletedBy).NotEmpty();
        RuleFor(x => x.Status).NotEmpty().Equal(SystemStatus.Deleted.GetHashCode());
    }
}
