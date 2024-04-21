using Microsoft.AspNetCore.Http;

namespace Application.ClientFeatures.Instructor.Request;

public sealed record InstructorCreateRequest
{
    public string FullName { get; set; }
    public string Gender { get; set; }
    public int Age { get; set; }
    public DateTime DateOfJoined { get; set; }
    public string Qualification { get; set; }
    public IFormFile Image { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int? CreatedBy { get; set; }
    public int Status { get; set; }
}

public sealed record InstructorUpdateRequest
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Gender { get; set; }
    public int Age { get; set; }
    public DateTime DateOfJoined { get; set; }
    public string Qualification { get; set; }
    public IFormFile Image { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int? UpdatedBy { get; set; }
    public int Status { get; set; }
}

public sealed record InstructorDeleteRequest
{
    public int Id { get; set; }
    public int? DeletedBy { get; set; }
    public int Status { get; set; }
}
