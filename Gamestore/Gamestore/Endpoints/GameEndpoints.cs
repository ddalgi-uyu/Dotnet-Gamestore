using System.Reflection.Metadata.Ecma335;
using Gamestore.Api.Entities;
using Gamestore.Data;
using Gamestore.Map;
using GameStore.Api.Dtos;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndPointName = "GetGame";

    private static readonly List<GameSummaryDto> games = [
        new(
            1,
            "Street Fighter II",
            "Fighting",
            19.99M,
            new DateOnly(1992, 7, 15)
        ),
        new(
            2,
            "Final Fntasy XIV",
            "Rolepaying",
            59.99M,
            new DateOnly(2010, 9, 30)
        ),
        new(
            3,
        "FIFA 23",
        "Sports",
        69.99M,
        new DateOnly(2022, 9, 27))
    ];

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games").WithParameterValidation();

        // GET /games
        group.MapGet("/", (GameStoreContext dbContext) => 
            dbContext.Games
                .Include(game => game.Genre)
                .Select(game => game.ToGameSummaryDto())
                .AsNoTracking()
        );

        // GET /games/1
        group.MapGet("/{id}", (int id, GameStoreContext dbContext) => {
            // GameDto? game = games.Find(game => game.Id == id);
            Game? game = dbContext.Games.Find(id);

            return game is null ? Results.NotFound() : Results.Ok(game.ToGameDetailDto());
        })
        .WithName(GetGameEndPointName);

        // POST /games
        group.MapPost("/", (CreateGameDto newGame, GameStoreContext dbContext) => {
            // if (string.IsNullOrEmpty(newGame.Name)){
            //     return Results.BadRequest("Name is required");
            // }

            // GameDto game = new(
            //     games.Count + 1,
            //     newGame.Name,
            //     newGame.Genre,
            //     newGame.Price,
            //     newGame.ReleaseDate
            // );
            Game game = newGame.ToEntity();
            // game.Genre = dbContext.Genres.Find(newGame.GenreId);
            
            // games.Add(game);
            dbContext.Games.Add(game);
            dbContext.SaveChanges();

            return Results.CreatedAtRoute(
                GetGameEndPointName, 
                new { id = game.Id},
                game.ToGameDetailDto());
        });

        // PUT /games
        group.MapPut("/{id}", (int id, UpdateGameDto updateGame, GameStoreContext dbContext) => 
        {
            // var index = games.FindIndex(game => game.Id == id);
            var existingGame = dbContext.Games.Find(id);

            // if (index == -1) 
            // {
            //     return Results.NotFound();
            // }

            if (existingGame is null)
            {
                return Results.NotFound();
            }

            // games[index] = new GameSummaryDto(
            //     id,
            //     updateGame.Name,
            //     updateGame.Genre,
            //     updateGame.Price,
            //     updateGame.ReleaseDate
            // );

            dbContext.Entry(existingGame).CurrentValues.SetValues(updateGame.ToEntity(id));
            dbContext.SaveChanges();

            return Results.NoContent();
        });

        // DELETE /games/1
        group.MapDelete("/{id}", (int id, GameStoreContext dbContext) => {
            // games.RemoveAll(game => game.Id == id);
            dbContext.Games
                .Where(game => game.Id == id)
                .ExecuteDelete();
                
            return Results.NoContent(); 
            // or create one
            // but there might be issues related to id
        });

        return group;
    }
}
