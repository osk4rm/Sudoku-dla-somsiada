using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace SudokuAPI.Data.Mediator
{
    public interface IMediator
    {
        void Register<TRequest, TResponse>(IHandler<TRequest, TResponse> handler) where TRequest : IRequest<TResponse>;
        Task<TResponse> Send<TRequest, TResponse>(TRequest request) where TRequest : IRequest<TResponse>;
    }
}
