using System.Security.Cryptography.X509Certificates;
using BasicApi.Data;
using BasicApi.Entities;
using BasicApi.Mapping;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.EntityFrameworkCore;

namespace BasicApi.Endpoints;

public static class GenreEndpoints
{
    public static RouteGroupBuilder MapGenreEndpoints(this WebApplication app)
    {
        var group = app
            .MapGroup("/genres")
            .WithParameterValidation();

        // GET All Genres
        group.MapGet("/", async (GameContext dbContext) => 
        {
            return await dbContext
                .Genres
                .Select(genre => genre.ToDto())
                .AsNoTracking()
                .ToListAsync();
        });

        // GET Genre by id
        group.MapGet("/{id}", async(int id, GameContext dbContext) =>
        {
            Genre? entity = await dbContext.Genres.FindAsync(id);
            return entity is null ? Results.NoContent() : Results.Ok(entity.ToDto());
        });

        return group;
    }
}