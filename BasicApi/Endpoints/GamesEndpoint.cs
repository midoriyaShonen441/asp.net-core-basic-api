using BasicApi.Data;
using BasicApi.Dtos;
using BasicApi.Entities;
using BasicApi.Mapping;
using Microsoft.EntityFrameworkCore;

namespace BasicApi.Endpoints;

public static class GamesEndpoint
{
    const string GetGameEndPointName = "GetGame";

    private static readonly List<GameSummaryDto> _games = [
        new (
            1,
            "Street Fighter II",
            "Fighting",
            19.99M,
            new DateOnly(1992, 7, 15)),
        new (
            2,
            "Final Fantasy XIV",
            "RolePlaying",
            59.99M,
            new DateOnly(2010, 9, 30)),
        new(
            3,
            "FIFA 23",
            "Sports",
            69.99M,
            new DateOnly(2022, 9 ,27))
    ];

    public static RouteGroupBuilder MapGamesEndpoint(this WebApplication app)
    {
        var group = app.MapGroup("games")
                .WithParameterValidation();

        // GET /games
        group.MapGet("/", async (GameContext dbContext) => 
        {
            await dbContext
                .Games
                .Include(game => game.Genre)
                .Select(game => game.ToGameSummaryDto())
                .AsNoTracking()  // No Tracking make EF just return data to client.
                .ToListAsync();
        });

        // GET /games/{id}
        group.MapGet("/{id}", async (int id, GameContext dbContext) => 
        {
            Game? game = await dbContext.Games.FindAsync(id);

            return game is null ? 
                Results.NotFound() : Results.Ok(game.ToGameDetailsDto());
        })
        .WithName(GetGameEndPointName);

        // POST /games
        group.MapPost("/", async (CreateGameDto newGame, GameContext dbContext) => 
        {
            if (string.IsNullOrEmpty(newGame.Name))
            {
                return Results.BadRequest("Name is required");
            }

            Game game = newGame.ToEntity();
            game.Genre = dbContext.Genres.Find(newGame.GenreId);

            await dbContext.Games.AddAsync(game);
            await dbContext.SaveChangesAsync();

            GameSummaryDto gameSummaryDto = game.ToGameSummaryDto();
            return Results.CreatedAtRoute(GetGameEndPointName, new { id = game.Id }, gameSummaryDto);
        });

        // PUT /games/{id}
        group.MapPut("/{id}", async (int id, UpdateGameDto updatedGame, GameContext dbContext) => 
        {
            var existingGame = await dbContext.Games.FindAsync(id);
            if (existingGame is null)
            {
                return Results.NotFound();
            }

            dbContext.Entry(existingGame)
                .CurrentValues
                .SetValues(updatedGame.ToEntity(id));

            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });

        //  DELETE /games/{id}
        group.MapDelete("/{id}", async (int id, GameContext dbContext) =>
        {
            await dbContext
                .Games
                .Where(game => game.Id == id)
                .ExecuteDeleteAsync();

            return Results.NoContent();
        });

        return group;
    }
}