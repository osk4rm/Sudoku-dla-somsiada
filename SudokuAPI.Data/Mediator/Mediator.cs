using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SudokuAPI.Data.Mediator
{
    public class Mediator : IMediator
    {
        private readonly Dictionary<Type, object> _handlers = new();

        public void Register<TRequest, TResponse>(IHandler<TRequest, TResponse> handler) where TRequest : IRequest<TResponse>
        {
            _handlers[typeof(TRequest)] = handler;
        }

        public async Task<TResponse> Send<TRequest, TResponse>(TRequest request) where TRequest : IRequest<TResponse>
        {
            if (_handlers.TryGetValue(request.GetType(), out var handler))
            {
                return await ((IHandler<TRequest, TResponse>)handler).Handle(request);
            }

            throw new InvalidOperationException("Handler not found");
        }
    }
}
