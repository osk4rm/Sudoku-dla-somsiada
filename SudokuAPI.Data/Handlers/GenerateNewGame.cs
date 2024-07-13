using SudokuAPI.Data.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuAPI.Data.Handlers
{
    public class GenerateNewGame
    {
        public sealed record Request : IRequest<List<List<int>>>;

        public sealed class Handler : IHandler<Request, List<List<int>>>
        {
            public Task<List<List<int>>> Handle(Request request)
            {
                var game = new SudokuGame();
                return Task.FromResult(game.GetBoardAsList());
            }
        }
    }
}
