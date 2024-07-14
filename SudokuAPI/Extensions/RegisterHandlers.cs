using SudokuAPI.Data.Observers;
using SudokuAPI.Data;
using SudokuAPI.Data.Mediator;
using SudokuAPI.Data.Handlers;

namespace SudokuAPI.Extensions
{
    public static class RegisterHandlersExtensions
    {
        public static WebApplication RegisterHandlers(this WebApplication app)
        {
            var mediator = app.Services.GetRequiredService<IMediator>();

            var observer = app.Services.GetRequiredService<IGameObserver>();
            var game = app.Services.GetRequiredService<SudokuGame>();

            var generateHandler = new GenerateNewGame.Handler(game);
            var checkHandler = new CheckNumberRequest.Handler(game, observer);
            mediator.Register(generateHandler);
            mediator.Register(checkHandler);

            return app;
        }
    }
}
