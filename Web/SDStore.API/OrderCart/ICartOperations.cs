namespace SDStore.API.OrderCart
{
    using Shared.DataTransferObjects.Response;
    
    public interface ICartOperations : IOrderMetadata
    {
        void AddItem(int id, decimal amount);
        void UpdateItemAmount(int id, decimal amount);
        void RemoveItems(int id);
        void Clear();
        
        Task<Response_OrderDetails> PlaceOrder(CancellationToken cancellationToken);
    }
}