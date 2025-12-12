namespace SimpleOOP.Data
{
    using Calculators;
    using SDStore.Data.Shared.Enums;
    
    public class OrderItem
    {
        private readonly Calculator _calculator;
        private readonly Currency _orderCurrency;
        
        public OrderItem(Item item, decimal amount, Currency orderCurrency, Calculator calculator)
        {
            _calculator = calculator;
            _orderCurrency = orderCurrency;
            
            Item = item;
            Amount = amount;
        }
        
        public Item Item { get; }
        public decimal Amount { get; private set; }

        public decimal SubTotal_Gross => _calculator.CalculateSubTotal_Gross(Item.Price, Amount, from: Item.Currency, to: _orderCurrency);
        public decimal SubTotal_VAT_Excluded => _calculator.CalculateSubTotal_VAT_Excluded(SubTotal_Gross, Item.VAT_Percentage);
        public decimal SubTotal_Net => _calculator.CalculateSubTotal_Net(SubTotal_Gross, SubTotal_VAT_Excluded);
        
        public void IncrementAmount(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Invalid amount");
            }

            Amount += amount;
        }
        
        public void UpdateAmount(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Invalid amount");
            }

            Amount = amount;
        }
    }
}