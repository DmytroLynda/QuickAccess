using AutoMapper;
using Data.Internal.Exceptions;
using Data.Internal.Interfaces;
using Newtonsoft.Json;
using Server.DTOs;
using Server.DTOs.ResponseTypes;
using Server.Enums;
using System;
using System.Text;

namespace Data.Internal.Preprocessors
{
    internal abstract class JsonOperationPreprocessor<TRequest, TResponse> : IOperationPreprocessor<TRequest, TResponse>
    {
        private readonly IMapper _mapper;

        public JsonOperationPreprocessor(IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public abstract byte[] Preprocess(TRequest request);

        public abstract TResponse Preprocess(byte[] responseBytes);

        protected byte[] Preprocess<TRequestDTO>(TRequest requestData, Query query)
        {
            var requestDataDTO = _mapper.Map<TRequestDTO>(requestData);
            var serializedRequestData = JsonConvert.SerializeObject(requestDataDTO);
            var requestDataBytes = Encoding.UTF8.GetBytes(serializedRequestData);

            var requestDTO = new RequestDTO
            {
                Query = query,
                Data = requestDataBytes
            };
            var serializedRequestDTO = JsonConvert.SerializeObject(requestDTO);
            return Encoding.UTF8.GetBytes(serializedRequestDTO);
        }

        protected TResponse Preprocess<TResponseDTO>(byte[] responseBytes, ResponseType responseType)
        {
            var serializedResponse = Encoding.UTF8.GetString(responseBytes);
            var response = JsonConvert.DeserializeObject<ResponseDTO>(serializedResponse);

            if (response.Type == responseType)
            {
                var serializedData = Encoding.UTF8.GetString(response.Data);
                var data = JsonConvert.DeserializeObject<TResponseDTO>(serializedData);
                return _mapper.Map<TResponse>(data);
            }
            else if (response.Type == ResponseType.Error)
            {
                var serializedError = Encoding.UTF8.GetString(response.Data);
                var error = JsonConvert.DeserializeObject<ErrorDTO>(serializedError);
                throw new ErrorResponseMessageException(error.Exception);
            }
            else
            {
                throw new InvalidOperationException($"Unknown response type: {response.Type}");
            }
        }
    }
}
