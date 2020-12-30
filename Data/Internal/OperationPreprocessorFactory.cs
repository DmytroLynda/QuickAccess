using AutoMapper;
using Data.Internal.Interfaces;
using Data.Internal.Preprocessors;
using DomainEntities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data
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
