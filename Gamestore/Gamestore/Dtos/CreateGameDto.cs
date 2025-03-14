using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dtos;

public record class CreateGameDto(
    // Use with Endpoints filter
    [Required][StringLength(50)] string Name,
    [Required][StringLength(50)] string Genre,
    [Range(1, 100)] decimal Price,
    DateOnly ReleaseDate
);