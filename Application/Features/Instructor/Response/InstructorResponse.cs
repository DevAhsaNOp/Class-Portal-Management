namespace Application.ClientFeatures.Instructor.Response;

public sealed record InstructorResponse
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Gender { get; set; }
    public int Age { get; set; }
    public DateTime DateOfJoined { get; set; }
    public string Qualification { get; set; }
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

public sealed record InstructorResponseV2
{
    public string FullName { get; set; }
    public string Gender { get; set; }
    public int Age { get; set; }
    public DateTime DateOfJoined { get; set; }
    public string Qualification { get; set; }
    public string Image { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public int Status { get; set; }
    public DateTime? CreatedAt { get; set; }
}

