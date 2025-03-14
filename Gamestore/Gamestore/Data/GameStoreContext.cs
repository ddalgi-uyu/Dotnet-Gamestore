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
}
