namespace Application.ClientFeatures.Enrollment.Response;

public sealed record EnrollmentResponse
{
    public int Id { get; set; }
    public int UserID { get; set; }
    public string UserName { get; set; }
    public int ClassID { get; set; }
    public string ClassName { get; set; }
    public int InstructorID { get; set; }
    public string InstructorName { get; set; }
    public DateTime EnrollmentDate { get; set; }
    public int Status { get; set; }
    public DateTime? CreatedAt { get; set; }
    public int? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? UpdatedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
    public int? DeletedBy { get; set; }
}

