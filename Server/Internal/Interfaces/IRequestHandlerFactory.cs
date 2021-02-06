using Server.Enums;

namespace Server.Internal.Interfaces
{
    internal interface IRequestHandlerFactory
    {
        IRequestHandler Create(Query query);
    }
}
