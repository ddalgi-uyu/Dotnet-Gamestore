using System;
using Gamestore.Api.Entities;
using GameStore.Api.Dtos;

namespace Gamestore.Map;

public static class GameMapping
{
    public static Game ToEntity(this CreateGameDto game)
    {
        return new Game()
            {
                Name = game.Name,
                GenreId = game.GenreId,
                Price = game.Price,
                ReleaseDate = game.ReleaseDate
            };
    }

    public static Game ToEntity(this UpdateGameDto game, int id)
    {
        return new Game()
            {
                Id = id,
                Name = game.Name,
                GenreId = game.GenreId,
                Price = game.Price,
                ReleaseDate = game.ReleaseDate
            };
    }

    public static GameSummaryDto ToGameSummaryDto(this Game game)
    {
        return new (
                game.Id,
                game.Name,
                game.Genre!.Name, // Indicating it would never be null
                game.Price,
                game.ReleaseDate
            );
    }

    public static GameDetailDto ToGameDetailDto(this Game game)
    {
        return new (
                game.Id,
                game.Name,
                game.GenreId,
                game.Price,
                game.ReleaseDate
            );
    }
}
