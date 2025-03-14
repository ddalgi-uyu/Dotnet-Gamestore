using GameStore.Api.Entities;

namespace Gamestore.Api.Entities;

public class Game
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public int GenreId { get; set;} // Used by Entitie Framework Core

    public Genre? Genre { get; set; }

    public decimal Price { get; set;}

    public DateOnly ReleaseDate { get; set; }
}