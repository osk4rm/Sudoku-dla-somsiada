using Microsoft.AspNetCore.Components.Forms;
using SudokuAPI.Data;
using SudokuAPI.Data.Handlers;
using SudokuAPI.Data.Mediator;
using SudokuAPI.Data.Observers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IMediator, Mediator>();
builder.Services.AddSingleton<IGameObserver, GameObserver>();
builder.Services.AddSingleton<SudokuGame>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var mediator = app.Services.GetRequiredService<IMediator>();
var observer = app.Services.GetRequiredService<IGameObserver>();
var game = app.Services.GetRequiredService<SudokuGame>();

game.AddObserver(observer);

var generateHandler = new GenerateNewGame.Handler();
var checkHandler = new CheckNumberRequest.Handler(game);
mediator.Register(generateHandler);
mediator.Register(checkHandler);

app.MapPost("/generate", async () =>
{
    var request = new GenerateNewGame.Request();
    var result = await mediator.Send<GenerateNewGame.Request, List<List<int>>>(request);

    observer.Notify("new game generated");

    return Results.Ok(result);
})
    .WithOpenApi();

app.MapPost("/check", async (int row, int col, int number) =>
{
    var request = new CheckNumberRequest.Request(row, col, number);
    var result = await mediator.Send<CheckNumberRequest.Request, bool>(request);

    return Results.Ok(result);
})
    .WithOpenApi();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
