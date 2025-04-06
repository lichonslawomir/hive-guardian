using HiveGuardian.Models;
using Microsoft.EntityFrameworkCore;

namespace HiveGuardian.Data;

public class HiveDbContext : DbContext
{
    public DbSet<SensorData> SensorData => Set<SensorData>();

    public HiveDbContext(DbContextOptions<HiveDbContext> options) : base(options) { }
}