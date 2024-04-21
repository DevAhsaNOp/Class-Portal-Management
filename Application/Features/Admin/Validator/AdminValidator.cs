using Application.ClientFeatures.Admin.Request;
using FluentValidation;
using static Application.Common.Behaviors.Statuses;

namespace Application.ClientFeatures.Admin.Validator;

public sealed class AdminCreateRequestValidator : AbstractValidator<AdminCreateRequest>
{
    public AdminCreateRequestValidator()
    {
        RuleFor(x => x.FullName).NotNull().NotEmpty().MaximumLength(150);
        RuleFor(x => x.Image).NotNull();
        RuleFor(x => x.Username).NotNull().NotEmpty().MaximumLength(50);
        RuleFor(x => x.Email).NotNull().NotEmpty().MaximumLength(50);
        RuleFor(x => x.Password).NotNull().NotEmpty().MaximumLength(50);
        RuleFor(x => x.CreatedBy).NotEmpty().NotEqual(0);
        RuleFor(x => x.Status).NotEmpty().NotEqual(0).LessThanOrEqualTo(SystemStatus.InActive.GetHashCode());
    }
}

public sealed class AdminUpdateRequestValidator : AbstractValidator<AdminUpdateRequest>
{
    public AdminUpdateRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotEqual(0);
        RuleFor(x => x.FullName).NotNull().NotEmpty().MaximumLength(150);
        RuleFor(x => x.Image).NotNull();
        RuleFor(x => x.Username).NotNull().NotEmpty().MaximumLength(50);
        RuleFor(x => x.Email).NotNull().NotEmpty().MaximumLength(50);
        RuleFor(x => x.Password).NotNull().NotEmpty().MaximumLength(50);
        RuleFor(x => x.UpdatedBy).NotEmpty().NotEqual(0);
        RuleFor(x => x.Status).NotEmpty().NotEqual(0).LessThanOrEqualTo(SystemStatus.InActive.GetHashCode());
    }
}

public sealed class AdminDeleteRequestValidator : AbstractValidator<AdminDeleteRequest>
{
    public AdminDeleteRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotEqual(0);
        RuleFor(x => x.DeletedBy).NotEmpty();
        RuleFor(x => x.Status).NotEmpty().Equal(SystemStatus.Deleted.GetHashCode());
    }
}
