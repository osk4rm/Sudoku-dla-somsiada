using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SudokuAPI.Data.Mediator
{
    public interface IHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        Task<TResponse> Handle(TRequest request);
    }
}
