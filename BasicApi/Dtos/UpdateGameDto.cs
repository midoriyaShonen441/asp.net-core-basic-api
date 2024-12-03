namespace BasicApi.Dtos;

public record class UpdateGameDto(
    string Name,
    int GenreId,
    decimal Price,
    DateOnly ReleaseDate);
