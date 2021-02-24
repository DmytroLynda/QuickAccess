using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackerWebApi.Model
{
    public class AuthenticationResponse
    {
        public string Id { get; set; }
        public string Token { get; set; }

        public AuthenticationResponse(string id, string token)
        {
            Id = id;
            Token = token;
        }
    }
}
