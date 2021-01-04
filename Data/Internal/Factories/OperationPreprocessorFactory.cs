using Data.Internal.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Data.Internal.Factories
{
    internal class OperationPreprocessorFactory : IOperationPreprocessorFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public OperationPreprocessorFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IOperationPreprocessor<TRequest, TResponse> Create<TRequest, TResponse>()
        {
            return _serviceProvider.GetRequiredService<IOperationPreprocessor<TRequest, TResponse>>();
        }
    }
}
