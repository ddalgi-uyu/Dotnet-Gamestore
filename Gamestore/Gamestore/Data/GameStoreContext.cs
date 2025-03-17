using Microsoft.EntityFrameworkCore;
using Gamestore.Api.Entities;
using GameStore.Api.Entities;

namespace Gamestore.Data;

public class GameStoreContext(DbContextOptions<GameStoreContext> options) : DbContext(options)
{
    // DbContext is an object that represents a session with the database
    // Can be used to query and save instances of entites

    public DbSet<Game> Games => Set<Game>(); // An object the can be used to query and save instances of game

    public DbSet<Genre> Genres => Set<Genre>();

    // Excuted while migration starts
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>().HasData(
            new { Id = 1, Name = "Fighting" },
            new { Id = 2, Name = "Roleplaying" },
            new { Id = 3, Name = "Sports" },
            new { Id = 4, Name = "Racing" },
            new { Id = 5, Name = "Kids and Family"}
        );
    }
}
