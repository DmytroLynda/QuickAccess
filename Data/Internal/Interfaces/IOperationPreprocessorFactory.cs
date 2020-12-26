namespace Data.Internal.Interfaces
{
    internal interface IOperationPreprocessorFactory
    {
        IOperationPreprocessor<TRequest, TResponse> Create<TRequest, TResponse>();
    }
}
