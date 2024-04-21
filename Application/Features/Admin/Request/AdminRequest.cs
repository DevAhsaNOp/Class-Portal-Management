using Microsoft.AspNetCore.Http;

namespace Application.ClientFeatures.Admin.Request;

public sealed record AdminCreateRequest
{
    public string FullName { get; set; }
    public IFormFile Image { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int? CreatedBy { get; set; }
    public int Status { get; set; }
}

public sealed record AdminUpdateRequest
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public IFormFile Image { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int? UpdatedBy { get; set; }
    public int Status { get; set; }
}

public sealed record AdminDeleteRequest
{
    public int Id { get; set; }
    public int? DeletedBy { get; set; }
    public int Status { get; set; }
}
