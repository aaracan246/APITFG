using Microsoft.EntityFrameworkCore;

namespace StatsApi.Models;

public class ScoreContext : DbContext
{
    public ScoreContext(DbContextOptions<ScoreContext> options)
        : base(options)
    {
    }

    public DbSet<Score> Score { get; set; } = null!;
}