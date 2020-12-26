using AutoMapper;
using Data.Internal.Interfaces;
using Data.Internal.Preprocessors;
using DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data
{
    internal class OperationPreprocessorFactory : IOperationPreprocessorFactory
    {
        private readonly List<(Type preprocessor, Type TRequest, Type TResponse)> _bindings;
        private readonly IMapper _mapper;

        public OperationPreprocessorFactory(IMapper mapper)
        {
            _bindings = BindPreprocessors();
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public IOperationPreprocessor<TRequest, TResponse> Create<TRequest, TResponse>()
        {
            var preprocessors = _bindings.Where(record => record.TRequest == typeof(TRequest) && record.TResponse == typeof(TResponse));

            if (!preprocessors.Any())
            {
                throw new ArgumentException("Preprocessor was not found.");
            }

            var preprocessorType = preprocessors.First().preprocessor;
            var preprocessor = (IOperationPreprocessor<TRequest, TResponse>) Activator.CreateInstance(preprocessorType, _mapper);

            return preprocessor;
        }

        private List<(Type preprocessor, Type TRequest, Type TResponse)> BindPreprocessors()
        {
            return new List<(Type preprocessor, Type TRequest, Type TResponse)>
            {
                (typeof(DownloadFileOperationPreprocessor), typeof(FilePath), typeof(File))
            };
        }
    }
}
