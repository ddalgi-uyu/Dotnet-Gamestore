using System;
using Gamestore.Data;
using Gamestore.Map;
using GameStore.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.Endpoints;

public static class GenreEndpoints
{
    public static RouteGroupBuilder MapGenresEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("genres");

        group.MapGet("/", async (GameStoreContext dbContext) => 
            await dbContext.Genres
                        .Select(genre => genre.ToDto())
                        .AsNoTracking()
                        .ToListAsync()
        );

        return group;
    }
}
