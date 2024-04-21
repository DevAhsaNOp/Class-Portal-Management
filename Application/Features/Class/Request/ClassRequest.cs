using Microsoft.AspNetCore.Http;

namespace Application.ClientFeatures.Class.Request;

public sealed record ClassCreateRequest
{
    public string ClassName { get; set; }
    public int GradeLevel { get; set; }
    public IFormFile Image { get; set; }
    public string Description { get; set; }
    public string AgeGroups { get; set; }
    public decimal Fees { get; set; }
    public DateTime StartTiming { get; set; }
    public DateTime EndTiming { get; set; }
    public int MaxClassSize { get; set; }
    public int InstructorID { get; set; }
    public int? CreatedBy { get; set; }
    public int Status { get; set; }
}

public sealed record ClassUpdateRequest
{
    public int Id { get; set; }
    public string ClassName { get; set; }
    public int GradeLevel { get; set; }
    public IFormFile Image { get; set; }
    public string Description { get; set; }
    public string AgeGroups { get; set; }
    public decimal Fees { get; set; }
    public DateTime StartTiming { get; set; }
    public DateTime EndTiming { get; set; }
    public int MaxClassSize { get; set; }
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
