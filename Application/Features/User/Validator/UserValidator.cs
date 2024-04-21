using Application.ClientFeatures.User.Request;
using FluentValidation;
using static Application.Common.Behaviors.Statuses;

namespace Application.ClientFeatures.User.Validator;

public sealed class UserCreateRequestValidator : AbstractValidator<UserCreateRequest>
{
    public UserCreateRequestValidator()
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

public sealed class UserLoginRequestValidator : AbstractValidator<UserLoginRequest>
{
    public UserLoginRequestValidator()
    {
        RuleFor(x => x.Email).NotNull().NotEmpty();
        RuleFor(x => x.Password).NotNull().NotEmpty();
    }
}

public sealed class UserUpdateRequestValidator : AbstractValidator<UserUpdateRequest>
{
    public UserUpdateRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotEqual(0);
        RuleFor(x => x.FullName).NotNull().NotEmpty().MaximumLength(150);
        RuleFor(x => x.Username).NotNull().NotEmpty().MaximumLength(50);
        RuleFor(x => x.Email).NotNull().NotEmpty().MaximumLength(50);
        RuleFor(x => x.UpdatedBy).NotEmpty().NotEqual(0);
        RuleFor(x => x.Status).NotEmpty().NotEqual(0).LessThanOrEqualTo(SystemStatus.InActive.GetHashCode());
    }
}

public sealed class UserChangePasswordRequestValidator : AbstractValidator<UserChangePasswordRequest>
{
    public UserChangePasswordRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotEqual(0);
        RuleFor(x => x.OldPassword).NotNull().NotEmpty().MaximumLength(50);
        RuleFor(x => x.Password).NotNull().NotEmpty().MaximumLength(50);
        RuleFor(x => x.ConfirmPassword).NotNull().NotEmpty().MaximumLength(50);
        RuleFor(x => x.UpdatedBy).NotEmpty().NotEqual(0);
        RuleFor(x => x.Status).NotEmpty().NotEqual(0).LessThanOrEqualTo(SystemStatus.InActive.GetHashCode());
    }
}

public sealed class UserDeleteRequestValidator : AbstractValidator<UserDeleteRequest>
{
    public UserDeleteRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotEqual(0);
        RuleFor(x => x.DeletedBy).NotEmpty();
        RuleFor(x => x.Status).NotEmpty().Equal(SystemStatus.Deleted.GetHashCode());
    }
}
