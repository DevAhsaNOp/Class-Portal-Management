namespace Application.ClientFeatures.Class.Response;

public sealed record ClassResponse
{
    public int Id { get; set; }
    public string ClassName { get; set; }
    public int GradeLevel { get; set; }
    public string Image { get; set; }
    public string Description { get; set; }
    public string AgeGroups { get; set; }
    public decimal Fees { get; set; }
    public DateTime StartTiming { get; set; }
    public DateTime EndTiming { get; set; }
    public int MaxClassSize { get; set; }
    public int InstructorID { get; set; }
    public string InstructorName { get; set; }
    public int Status { get; set; }
    public DateTime? CreatedAt { get; set; }
    public int? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? UpdatedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
    public int? DeletedBy { get; set; }
}

