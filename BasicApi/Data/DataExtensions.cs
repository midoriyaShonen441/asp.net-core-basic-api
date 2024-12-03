using Microsoft.EntityFrameworkCore;

namespace BasicApi.Data;

public static class DataExtensions
{
    // Extend app class
    public static async Task MigrateDbAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GameContext>();

        await dbContext.Database.MigrateAsync();
    }
}