using Application.ClientFeatures.Instructor.Response;
using Application.ClientFeatures.User.Response;

namespace Application.ClientFeatures.Admin.Response;

public sealed record AdminResponse
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Image { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public int Status { get; set; }
    public DateTime? CreatedAt { get; set; }
    public int? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? UpdatedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
    public int? DeletedBy { get; set; }
}

public sealed record UserAndInstructorDetails
{
    public List<UserResponseV2> Users { get; set; }
    public List<UserResponseV2> NonActiveUsers { get; set; }
    public List<InstructorResponseV2> Instructors { get; set; }
}

