using DAL.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.BackgroundServices
{
    public class MigrationService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public MigrationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using IServiceScope serviceScope = _serviceProvider.CreateScope();
            DbContextBase dbContext = serviceScope.ServiceProvider.GetRequiredService<DbContextBase>();
            await dbContext.Database.MigrateAsync(stoppingToken);
        }
    }

    public static class MigrationServiceExtensions
    {
        public static IServiceCollection AddMigrationService(this IServiceCollection services)
        {
            services.AddHostedService<MigrationService>();
            return services;
        }
    }
}
