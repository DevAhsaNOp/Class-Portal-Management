using System.ComponentModel.DataAnnotations;

namespace Application.ClientFeatures.Enrollment.Request;

public sealed record EnrollmentCreateRequest
{
    [Required(ErrorMessage = "*")]
    [Range(1, 100000, ErrorMessage = "Please select User")]
    public int UserID { get; set; }

    [Required(ErrorMessage = "*")]
    [Range(1, 100000, ErrorMessage = "Please select Class")]
    public int ClassID { get; set; }

    [Required(ErrorMessage = "*")]
    public DateTime EnrollmentDate { get; set; }
    public int? CreatedBy { get; set; }
    public int Status { get; set; }
}

public sealed record EnrollmentUpdateRequest
{
    [Required(ErrorMessage = "*")]
    public int Id { get; set; }

    [Required(ErrorMessage = "*")]
    [Range(1, 100000, ErrorMessage = "Please select User")]
    public int UserID { get; set; }

    [Required(ErrorMessage = "*")]
    [Range(1, 100000, ErrorMessage = "Please select Class")]
    public int ClassID { get; set; }

    [Required(ErrorMessage = "*")]
    public DateTime EnrollmentDate { get; set; }
    public int? UpdatedBy { get; set; }
    public int Status { get; set; }
}

public sealed record EnrollmentDeleteRequest
{
    public int Id { get; set; }
    public int? DeletedBy { get; set; }
    public int Status { get; set; }
}
