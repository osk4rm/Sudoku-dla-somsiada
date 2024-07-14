using Microsoft.AspNetCore.Components.Forms;
using SudokuAPI.Data;
using SudokuAPI.Data.Handlers;
using SudokuAPI.Data.Mediator;
using SudokuAPI.Data.Models;
using SudokuAPI.Data.Observers;
using SudokuAPI.Data.Utils;
using SudokuAPI.Extensions;
using SudokuAPI.Requests;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IMediator, Mediator>();
builder.Services.AddSingleton<IGameObserver, GameObserver>();
builder.Services.AddSingleton<SudokuGame>();
builder.Services.AddTransient<ISudokuGenerator, SudokuGenerator>();

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

app.RegisterObservers();
app.RegisterHandlers();
app.RegisterSudokuEndpoints();

app.Run();

