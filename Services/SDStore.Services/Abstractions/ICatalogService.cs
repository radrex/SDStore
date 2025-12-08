namespace SDStore.Services.Abstractions
{
    using Shared.DataTransferObjects.Response;
    using Shared.DataTransferObjects.Request;
    using SDStore.Shared.DataTransferObjects.Shared;
    
    public interface ICatalogService
    {
        Task<OperationResult<Response_Item>> GetItem(int id, CancellationToken ct = default);
        Task<OperationResult<IEnumerable<Response_Item>>> GetItems(CancellationToken ct = default);
        Task<OperationResult<Response_Item>> CreateItem(Request_ItemCreate request, CancellationToken ct = default);
        Task<OperationResult<Response_Item>> UpdateItem(int id, Request_ItemUpdate request, CancellationToken ct = default);
    }
}