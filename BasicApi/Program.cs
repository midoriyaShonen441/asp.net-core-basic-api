using BasicApi.Data;
using BasicApi.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("Game");
builder.Services.AddSqlite<GameContext>(connString);

var app = builder.Build();

// GET / 
app.MapGet("/", () => "project is running");

// /games/*
app.MapGamesEndpoint();

// /genres/*
app.MapGenreEndpoints();

await app.MigrateDbAsync();

app.Run();
