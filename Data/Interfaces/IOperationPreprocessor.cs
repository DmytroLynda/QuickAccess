using Server.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IOperationPreprocessor<TRequest, TResponse>
    {
        byte[] Preprocess(TRequest request);
        TResponse Preprocess(byte[] responseBytes);
    }
}
