namespace Data.Internal.Interfaces
{
    internal interface IOperationPreprocessor<TRequest, TResponse>
    {
        byte[] Preprocess(TRequest request);
        TResponse Preprocess(byte[] responseBytes);
    }
}
