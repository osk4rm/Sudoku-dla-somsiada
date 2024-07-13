using SudokuAPI.Data.Mediator;
using SudokuAPI.Data.Models;
using SudokuAPI.Data.Observers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuAPI.Data.Handlers
{
    public class CheckNumberRequest
    {
        public sealed record Request(int Row, int Col, int Number) : IRequest<CheckResultModel>;

        public sealed class Handler : IHandler<Request, CheckResultModel>
        {
            private readonly IGameObserver _observer;
            private readonly SudokuGame _game;

            public Handler(SudokuGame game, IGameObserver observer)
            {
                _game = game;
                _observer = observer;
            }

            public Task<CheckResultModel> Handle(Request request)
            {
                var isCorrect = _game.CheckNumber(request.Row, request.Col, request.Number);

                var result = new CheckResultModel
                {
                    IsCorrect = isCorrect,
                    IsGameFinished = _observer.IsGameFinished
                };

                return Task.FromResult(result);
            }
        }
    }
}
