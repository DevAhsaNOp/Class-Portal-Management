using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Application.ClientFeatures.Class.Request;

public sealed record ClassCreateRequest
{
    [Required(ErrorMessage = "*")]
    [RegularExpression("^[a-zA-Z0-9\\s]*$", ErrorMessage = "Only Alphabets, Numbers, and Special Characters are allowed.")]
    [StringLength(100, MinimumLength = 4, ErrorMessage = "Qualification should be between 4 to 100 characters.")]
    public string ClassName { get; set; }

    [Required(ErrorMessage = "*")]
    [Range(1, 12, ErrorMessage = "Grade Level should be between 1 to 12.")]
    public int GradeLevel { get; set; }

    [Required(ErrorMessage = "Image Required")]
    [DataType(DataType.Upload)]
    public IFormFile Image { get; set; }

    [Required(ErrorMessage = "*")]
    public string Description { get; set; }

    [Required(ErrorMessage = "*")]
    public string AgeGroups { get; set; }

    [Required(ErrorMessage = "*")]
    [Range(1, 100000, ErrorMessage = "Fees should be between 1 to 100000.")]
    public decimal Fees { get; set; }

    [Required(ErrorMessage = "*")]
    public DateTime StartTiming { get; set; }

    [Required(ErrorMessage = "*")]
    public DateTime EndTiming { get; set; }

    [Required(ErrorMessage = "*")]
    [Range(1, 100000, ErrorMessage = "Class Size should be between 1 to 100000.")]
    public int MaxClassSize { get; set; }

    [Required(ErrorMessage = "*")]
    [Range(1, 100000, ErrorMessage = "Please select Instructor")]
    public int InstructorID { get; set; }
    public int? CreatedBy { get; set; }
    public int Status { get; set; }
}

public sealed record ClassUpdateRequest
{
    public int Id { get; set; }

    [Required(ErrorMessage = "*")]
    [RegularExpression("^[a-zA-Z0-9\\s]*$", ErrorMessage = "Only Alphabets, Numbers, and Special Characters are allowed.")]
    [StringLength(100, MinimumLength = 4, ErrorMessage = "Qualification should be between 4 to 100 characters.")]
    public string ClassName { get; set; }

    [Required(ErrorMessage = "*")]
    [Range(1, 12, ErrorMessage = "Grade Level should be between 1 to 12.")]
    public int GradeLevel { get; set; }

    [Required(ErrorMessage = "Image Required")]
    [DataType(DataType.Upload)]
    public IFormFile Image { get; set; }

    public string ProfileImage { get; set; }

    [Required(ErrorMessage = "*")]
    public string Description { get; set; }

    [Required(ErrorMessage = "*")]
    public string AgeGroups { get; set; }

    [Required(ErrorMessage = "*")]
    [Range(1, 100000, ErrorMessage = "Fees should be between 1 to 100000.")]
    public decimal Fees { get; set; }

    [Required(ErrorMessage = "*")]
    public DateTime StartTiming { get; set; }

    [Required(ErrorMessage = "*")]
    public DateTime EndTiming { get; set; }

    [Required(ErrorMessage = "*")]
    [Range(1, 100000, ErrorMessage = "Class Size should be between 1 to 100000.")]
    public int MaxClassSize { get; set; }

    [Required(ErrorMessage = "*")]
    [Range(1, 100000, ErrorMessage = "Please select Instructor")]
    public int InstructorID { get; set; }
    public int? UpdatedBy { get; set; }
    public int Status { get; set; }
}

public sealed record ClassDeleteRequest
{
    public int Id { get; set; }
    public int? DeletedBy { get; set; }
    public int Status { get; set; }
}
