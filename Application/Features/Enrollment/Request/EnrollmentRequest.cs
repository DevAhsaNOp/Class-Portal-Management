namespace Application.ClientFeatures.Enrollment.Request;

public sealed record EnrollmentCreateRequest
{
    public int UserID { get; set; }
    public int ClassID { get; set; }
    public DateTime EnrollmentDate { get; set; }
    public int? CreatedBy { get; set; }
    public int Status { get; set; }
}

public sealed record EnrollmentUpdateRequest
{
    public int Id { get; set; }
    public int UserID { get; set; }
    public int ClassID { get; set; }
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
