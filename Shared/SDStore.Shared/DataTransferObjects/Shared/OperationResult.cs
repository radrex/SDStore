namespace SDStore.Shared.DataTransferObjects.Shared
{
    using System.Net;
    
    /// <summary>
    /// Doing this, because I don't want to create multiple exceptions.
    /// </summary>
    public record OperationResult<T>
    {
        public T? Data { get; init; }
        public HttpStatusCode StatusCode { get; init; }
    }
}