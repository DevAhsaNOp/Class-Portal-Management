namespace Application.ClientFeatures.User.Response;

public sealed record UserResponse
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

public sealed record UserResponseV2
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Image { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public int Status { get; set; }
    public DateTime? CreatedAt { get; set; }
}

