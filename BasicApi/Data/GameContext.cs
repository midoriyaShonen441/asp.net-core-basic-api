using BasicApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace BasicApi.Data;

public class GameContext(DbContextOptions<GameContext> options) : DbContext(options)
{
    public DbSet<Game> Games => Set<Game>();
    public DbSet<Genre> Genres => Set<Genre>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Seeding Genres Data.
        modelBuilder.Entity<Genre>().HasData(
            new {Id = 1, Name = "Fighting"},
            new {Id = 2, Name = "RolePlaying"},
            new {Id = 3, Name = "Sports"},
            new {Id = 4, Name = "Racing"},
            new {Id = 5, Name = "Kids & Family"}
        );

        base.OnModelCreating(modelBuilder);
    }
}