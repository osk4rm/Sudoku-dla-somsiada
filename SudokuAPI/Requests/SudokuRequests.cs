using SudokuAPI.Data.Handlers;
using SudokuAPI.Data.Mediator;
using SudokuAPI.Data.Models;
using SudokuAPI.Data.Observers;
using System.Reflection;

namespace SudokuAPI.Requests
{
    public static class SudokuRequests
    {
        public static WebApplication RegisterSudokuEndpoints(this WebApplication app)
        {
            app.MapPost("/generate", SudokuRequests.Generate)
                .WithOpenApi();

            app.MapPost("/check", SudokuRequests.CheckNumber)
                .Accepts<CheckNumberModel>("application/json")
                .WithOpenApi();

            return app;
        }

        private static async Task<IResult> Generate(IMediator mediator, IGameObserver observer)
        {
            var request = new GenerateNewGame.Request();

            var result = await mediator.Send<GenerateNewGame.Request, List<List<int>>>(request);

            observer.Notify(false);

            return Results.Ok(result);
        }

        private static async Task<IResult> CheckNumber(IMediator mediator, CheckNumberModel model)
        {
            var request = new CheckNumberRequest.Request(model.Row, model.Col, model.Number);
            var result = await mediator.Send<CheckNumberRequest.Request, CheckResultModel>(request);

            return Results.Ok(result);
        }
    }
}
