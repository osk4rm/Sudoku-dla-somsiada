using SudokuAPI.Data.Observers;
using SudokuAPI.Data;

namespace SudokuAPI.Extensions
{
    public static class RegisterObserversExtensions
    {
        public static WebApplication RegisterObservers(this WebApplication app)
        {
            var observer = app.Services.GetRequiredService<IGameObserver>();
            var game = app.Services.GetRequiredService<SudokuGame>();

            game.AddObserver(observer);

            return app;
        }
    }
}
