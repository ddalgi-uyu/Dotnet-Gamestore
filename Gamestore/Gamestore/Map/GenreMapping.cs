using System;
using Gamestore.Dtos;
using GameStore.Api.Entities;

namespace Gamestore.Map;

public static class GenreMapping
{
    public static GenreDto ToDto(this Genre genre)
    {
        return new GenreDto(genre.Id, genre.Name);
    }
}
