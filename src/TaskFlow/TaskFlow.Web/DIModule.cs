using Microsoft.EntityFrameworkCore;
using TaskFlow.Application;
using TaskFlow.Application.Services;
using TaskFlow.Domain;
using TaskFlow.Domain.RepositoryContracts;
using TaskFlow.Infrastructure.Repositories;
using TaskFlow.Infrastructure.UnitOfWorks;
using TaskFlow.Web.Data;

namespace TaskFlow.Web
{
    public static class DIModule
    {
        public static void ServiceRegistration(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options => 
                options.UseNpgsql(connectionString));

            services.AddScoped<ITaskFlowUnitOfWork, TaskFlowUnitOfWork>();

            services.AddScoped<IStatusService, StatusService>();
            services.AddScoped<IStatusRepository, StatusRepository>();
        }
    }
}
