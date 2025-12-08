namespace SDStore.API.OrderCart
{
    using SDStore.Data.Shared.Enums;
    
    public interface IOrderMetadata
    {
        string Email { get; }
        string Address { get; }
        string PhoneNumber { get; }
        Currency Currency { get; }
        
        void SetEmail(string email);
        void SetAddress(string address);
        void SetPhoneNumber(string phoneNumber);
        void SetCurrency(Currency currency);
    }
}