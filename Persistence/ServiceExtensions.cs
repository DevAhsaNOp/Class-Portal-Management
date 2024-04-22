using Application.Interfaces;
using Application.Interfaces.Base;
using Application.Interfaces.ClientInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Persistence.Context;
using Persistence.Repositories;
using Persistence.Repositories.ClientRepositories;
using Persistence.Repositories.Common;

namespace Persistence
{
    public static class ServiceExtensions
    {
        public static void ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<DataContext>(opt => opt.UseSqlServer(connectionString));

            services.AddScoped<IUser, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAdmin, AdminRepository>();
            services.AddScoped<IClass, ClassRepository>();
            services.AddScoped<IHelper, HelperRepository>();
            services.AddScoped<IInstructor, InstructorRepository>();
            services.AddScoped<ICustomLogHandler>(_ => new CustomLogHandler("Logs"));
        }
    }
}
