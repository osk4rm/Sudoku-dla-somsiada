using SudokuAPI.Data.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuAPI.Data.Handlers
{
    public class CheckNumberRequest
    {
        public sealed record Request(int Row, int Col, int Number) : IRequest<bool>;

        public sealed class Handler : IHandler<Request, bool>
        {
            private readonly SudokuGame _game;

            public Handler(SudokuGame game)
            {
                _game = game;
            }

            public Task<bool> Handle(Request request)
            {
                var isCorrect = _game.CheckNumber(request.Row, request.Col, request.Number);
                return Task.FromResult(isCorrect);
            }
        }
    }
}
