namespace Application.ClientFeatures.Enrollment.Response;

public sealed record EnrollmentResponse
{
    public int Id { get; set; }
    public int UserID { get; set; }
    public string UserName { get; set; }
    public string UserImage { get; set; }
    public int ClassID { get; set; }
    public string ClassName { get; set; }
    public string ClassImage { get; set; }
    public string StartTiming { get; set; }
    public string EndTiming { get; set; }
    public string Description { get; set; }
    public int GradeLevel { get; set; }
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

public sealed record EnrollmentResponseV2
{
    public int ClassID { get; set; }
    public string ClassName { get; set; }
    public string AgeGroups { get; set; }
    public string ClassImage { get; set; }
    public int GradeLevel { get; set; }
    public int InstructorID { get; set; }
    public string InstructorName { get; set; }
    public int Status { get; set; }
    public List<UserEnrolledInfo> Users { get; set; }
}

public sealed record UserEnrolledInfo
{
    public int Id { get; set; }
    public int UserID { get; set; }
    public string Username { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string UserImage { get; set; }
    public string EnrollmentDate { get; set; }
    public int Status { get; set; }
}

