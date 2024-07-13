using Microsoft.AspNetCore.Components.Forms;
using SudokuAPI.Data;
using SudokuAPI.Data.Handlers;
using SudokuAPI.Data.Mediator;
using SudokuAPI.Data.Models;
using SudokuAPI.Data.Observers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IMediator, Mediator>();
builder.Services.AddSingleton<IGameObserver, GameObserver>();
builder.Services.AddSingleton<SudokuGame>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        builder =>
        {
            builder.WithOrigins("http://localhost:8080")
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials();
        });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowLocalhost");

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

var generateHandler = new GenerateNewGame.Handler(game);
var checkHandler = new CheckNumberRequest.Handler(game, observer);
mediator.Register(generateHandler);
mediator.Register(checkHandler);

app.MapPost("/generate", async () =>
{
    var request = new GenerateNewGame.Request();
    var result = await mediator.Send<GenerateNewGame.Request, List<List<int>>>(request);

    observer.Notify(false);

    return Results.Ok(result);
})
    .WithOpenApi();

app.MapPost("/check", async (CheckNumberModel model) =>
{
    var request = new CheckNumberRequest.Request(model.Row, model.Col, model.Number);
    var result = await mediator.Send<CheckNumberRequest.Request, CheckResultModel>(request);

    return Results.Ok(result);
})
    .WithOpenApi();

app.Run();

