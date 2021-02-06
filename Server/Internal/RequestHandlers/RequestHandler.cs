using Newtonsoft.Json;
using Server.DTOs;
using Server.Enums;
using System.Text;
using System.Threading.Tasks;

namespace Server.Internal.RequestHandlers
{
    internal abstract class RequestHandler : IRequestHandler
    {
        public abstract Task<byte[]> HandleAsync(byte[] data);

        protected TRequest GetRequest<TRequest>(byte[] request)
        {
            var serializedRequest = Encoding.UTF8.GetString(request);
            return JsonConvert.DeserializeObject<TRequest>(serializedRequest);
        }

        protected byte[] FormResponse(object responseData, ResponseType type)
        {
            var serializedResponseData = JsonConvert.SerializeObject(responseData);
            var responseDataBytes = Encoding.UTF8.GetBytes(serializedResponseData);

            var responseDTO = new ResponseDTO
            {
                Type = type,
                Data = responseDataBytes
            };

            var serializedResponse = JsonConvert.SerializeObject(responseDTO);
            return Encoding.UTF8.GetBytes(serializedResponse);
        }
    }
}
