using AutoMapper;
using Data.Internal.Interfaces;
using DomainEntities;
using Newtonsoft.Json;
using Server.DTOs;
using Server.DTOs.ResponseTypes;
using Server.Enums;
using System;
using System.Text;

namespace Data.Internal.Preprocessors
{
    internal class DownloadFileOperationPreprocessor : IOperationPreprocessor<FilePath, File>
    {
        private readonly IMapper _mapper;

        public DownloadFileOperationPreprocessor(IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public byte[] Preprocess(FilePath request)
        {
            var serializedFilePath = JsonConvert.SerializeObject(request);
            var filePathBytes = Encoding.UTF8.GetBytes(serializedFilePath);

            var requestDTO = new RequestDTO
            {
                Query = Query.DownloadFile,
                Request = filePathBytes
            };
            var serializedRequestDTO = JsonConvert.SerializeObject(requestDTO);
            return Encoding.UTF8.GetBytes(serializedRequestDTO);
        }

        public File Preprocess(byte[] responseBytes)
        {
            var serializedResponse = Encoding.UTF8.GetString(responseBytes);
            var response = JsonConvert.DeserializeObject<ResponseDTO>(serializedResponse);

            if (response.Type == ResponseType.File)
            {
                var serializedData = Encoding.UTF8.GetString(response.Data);
                var data = JsonConvert.DeserializeObject<FileResponseDTO>(serializedData);
                return _mapper.Map<File>(data);
            }
            else if (response.Type == ResponseType.Error)
            {
                var serializedError = Encoding.UTF8.GetString(response.Data);
                var error = JsonConvert.DeserializeObject<ErrorDTO>(serializedError);
                throw new ErrorResponseMessageException(error.Message);
            }
            else
            {
                throw new InvalidOperationException($"Unknown response type: {response.Type}");
            }
        }
    }
}
