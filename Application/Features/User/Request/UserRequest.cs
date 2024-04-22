using Application.Common.Behaviours;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Application.ClientFeatures.User.Request;

public sealed record UserCreateRequest
{
    [Required(ErrorMessage = "*")]
    [RegularExpression("^[a-zA-Z\\s]*$", ErrorMessage = "Only Alphabets are allowed.")]
    [StringLength(150, MinimumLength = 4, ErrorMessage = "Full Name should be between 4 to 150 characters.")]
    public string FullName { get; set; }

    [Required(ErrorMessage = "Image Required")]
    [DataType(DataType.Upload)]
    public IFormFile Image { get; set; }

    [Required(ErrorMessage = "*")]
    [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only Alphabets and Numbers are allowed.")]
    [StringLength(50, MinimumLength = 4, ErrorMessage = "Username should be between 4 to 50 characters.")]
    public string Username { get; set; }

    [Required(ErrorMessage = "*")]
    [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$", ErrorMessage = "Invalid Email Address")]
    [StringLength(50, MinimumLength = 4, ErrorMessage = "Email should be between 4 to 150 characters.")]
    [Remote("IsEmailExist", "Account", ErrorMessage = "Email already exists.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "*")]
    [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$",
        ErrorMessage = "Minimum 8 characters with upper/lower, digit, special.")]
    [StringLength(50, MinimumLength = 8, ErrorMessage = "Password should be between 8 to 50 characters.")]
    public string Password { get; set; }

    [Required(ErrorMessage = "*")]
    [Compare("Password", ErrorMessage = "Password do not match.")]
    [StringLength(50, MinimumLength = 8, ErrorMessage = "Confirm Password should be between 8 to 50 characters.")]
    public string ConfirmPassword { get; set; }

    public int? CreatedBy { get; set; }
    public int Status { get; set; }
}

public sealed record UserUpdateRequest
{
    public int Id { get; set; }

    [Required(ErrorMessage = "*")]
    [RegularExpression("^[a-zA-Z\\s]*$", ErrorMessage = "Only Alphabets are allowed.")]
    [StringLength(150, MinimumLength = 4, ErrorMessage = "Full Name should be between 4 to 150 characters.")]
    public string FullName { get; set; }

    [Required(ErrorMessage = "Image Required")]
    [DataType(DataType.Upload)]
    public IFormFile Image { get; set; }

    public string ProfileImage { get; set; }

    [Required(ErrorMessage = "*")]
    [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only Alphabets and Numbers are allowed.")]
    [StringLength(50, MinimumLength = 4, ErrorMessage = "Username should be between 4 to 50 characters.")]
    public string Username { get; set; }

    [Required(ErrorMessage = "*")]
    [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$", ErrorMessage = "Invalid Email Address")]
    [StringLength(50, MinimumLength = 4, ErrorMessage = "Email should be between 4 to 150 characters.")]
    //[Remote("IsEmailExist", "Account", ErrorMessage = "Email already exists.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "*")]
    [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$",
        ErrorMessage = "Minimum 8 characters with upper/lower, digit, special.")]
    [StringLength(50, MinimumLength = 8, ErrorMessage = "Password should be between 8 to 50 characters.")]
    public string Password { get; set; }

    [Required(ErrorMessage = "*")]
    [Compare("Password", ErrorMessage = "Password do not match.")]
    [StringLength(50, MinimumLength = 8, ErrorMessage = "Confirm Password should be between 8 to 50 characters.")]
    public string ConfirmPassword { get; set; }

    public int? UpdatedBy { get; set; }
    public int Status { get; set; }
}


public sealed record UserUpdateView
{
    public int Id { get; set; }
    public string ProfileImage { get; set; }

    [Required(ErrorMessage = "*")]
    [RegularExpression("^[a-zA-Z\\s]*$", ErrorMessage = "Only Alphabets are allowed.")]
    [StringLength(150, MinimumLength = 4, ErrorMessage = "Full Name should be between 4 to 150 characters.")]
    public string FullName { get; set; }

    public IFormFile? Image { get; set; }

    [Required(ErrorMessage = "*")]
    [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only Alphabets and Numbers are allowed.")]
    [StringLength(50, MinimumLength = 4, ErrorMessage = "Username should be between 4 to 50 characters.")]
    public string Username { get; set; }

    [Required(ErrorMessage = "*")]
    [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$", ErrorMessage = "Invalid Email Address")]
    [StringLength(50, MinimumLength = 4, ErrorMessage = "Email should be between 4 to 150 characters.")]
    public string Email { get; set; }

    public int Status { get; set; }
    public int? UpdatedBy { get; set; }
}

public sealed record UserChangePasswordRequest
{
    public int Id { get; set; }

    [Required(ErrorMessage = "*")]
    public string OldPassword { get; set; }

    [Required(ErrorMessage = "*")]
    [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$",
        ErrorMessage = "Minimum 8 characters with upper/lower, digit, special.")]
    [NotEqual("OldPassword", ErrorMessage = "Old Password and New Password can not be same.")]
    [StringLength(50, MinimumLength = 8, ErrorMessage = "Password should be between 8 to 50 characters.")]
    public string Password { get; set; }

    [Required(ErrorMessage = "*")]
    [Compare("Password", ErrorMessage = "Password do not match.")]
    [StringLength(50, MinimumLength = 8, ErrorMessage = "Password should be between 8 to 50 characters.")]
    public string ConfirmPassword { get; set; }

    public int Status { get; set; }
    public int? UpdatedBy { get; set; }
}


public sealed record UserLoginRequest
{
    [Required(ErrorMessage = "*")]
    public string Email { get; set; }

    [Required(ErrorMessage = "*")]
    public string Password { get; set; }
    public bool RememberMe { get; set; }
}

public sealed record UserDeleteRequest
{
    public int Id { get; set; }
    public int? DeletedBy { get; set; }
    public int Status { get; set; }
}
