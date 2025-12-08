namespace SDStore.Services.Abstractions
{
    using Shared.DataTransferObjects.Shared;
    using Shared.DataTransferObjects.Response;
    using Shared.DataTransferObjects.Request;
    
    public interface IOrdersService
    {
        Task<OperationResult<Response_OrderDetails>> GetOrder(Guid id, CancellationToken ct = default);
        Task<OperationResult<IEnumerable<Response_OrderDetails>>> GetOrders(string email, CancellationToken ct = default);
        Task<OperationResult<IEnumerable<Response_OrderDetails>>> GetOrders(CancellationToken ct = default);
        Task<OperationResult<IEnumerable<Response_OrderDetails>>> GetOrders(IEnumerable<Guid> ids, CancellationToken ct = default);
        Task<OperationResult<Response_OrderDetails>> PlaceOrder(Request_OrderDetails order, CancellationToken ct = default);
    }
}