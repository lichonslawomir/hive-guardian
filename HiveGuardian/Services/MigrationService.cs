using HiveGuardian.Data;
using Microsoft.EntityFrameworkCore;

namespace HiveGuardian.Services;

public class MigrationService(IServiceProvider serviceProvider, IConfiguration configuration) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var dbPath = configuration.GetConnectionString(nameof(HiveDbContext));
        var dir = Path.GetDirectoryName(dbPath);
        if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        using var scope = serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<HiveDbContext>();

        await db.Database.MigrateAsync(stoppingToken);
    }
}