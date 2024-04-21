using Domain.DbEntities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Context;

public class DataContext : DbContext
{
    private readonly HttpContext _httpContext;

    public DataContext(DbContextOptions<DataContext> options, IHttpContextAccessor? httpContextAccessor = null) : base(options)
    {
        _httpContext = httpContextAccessor?.HttpContext;
    }

    public DbSet<tblAdmin> tblAdmin { get; set; }
    public DbSet<tblEnrollment> tblEnrollment { get; set; }
    public DbSet<tblInstructor> tblInstructor { get; set; }
    public DbSet<tblUser> tblUser { get; set; }
    public DbSet<tblClass> tblClass { get; set; }
}