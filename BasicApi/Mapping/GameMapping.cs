using System.Diagnostics;
using BasicApi.Dtos;
using BasicApi.Entities;

namespace BasicApi.Mapping;

public static class GameMapping
{
    public static Game ToEntity(this CreateGameDto dto)
    {
        return new Game()
        {
            Name = dto.Name,
            GenreId = dto.GenreId,
            Price = dto.Price,
            ReleaseDate = dto.ReleaseDate
        };
    }

    public static Game ToEntity(this UpdateGameDto dto, int Id)
    {
        return new Game()
        {
            Id = Id,
            Name = dto.Name,
            GenreId = dto.GenreId,
            Price = dto.Price,
            ReleaseDate = dto.ReleaseDate
        };
    }

    public static GameDetailsDto ToGameDetailsDto(this Game game)
    {
        return new GameDetailsDto(
            game.Id,
            game.Name,
            game.GenreId,
            game.Price,
            game.ReleaseDate
        );
    }

    public static GameSummaryDto ToGameSummaryDto(this Game game)
    {
        return new GameSummaryDto(
            game.Id,
            game.Name,
            game.Genre!.Name,
            game.Price,
            game.ReleaseDate
        );
    }
}