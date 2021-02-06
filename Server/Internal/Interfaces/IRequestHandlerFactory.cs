using ServerInterface.Enums;

namespace ServerInterface.Internal.Interfaces
{
    internal interface IRequestHandlerFactory
    {
        IRequestHandler Create(Query query);
    }
}
