using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackerWebApi.Model
{
    public class RegistrationResponse
    {
        public string Id { get; set; }

        public RegistrationResponse(string id)
        {
            Id = id;
        }
    }
}
