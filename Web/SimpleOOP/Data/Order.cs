namespace SimpleOOP.Data
{
    using Calculators;
    using SDStore.Data.Shared.Enums;
    
    public class Order
    {
        private readonly List<OrderItem> _items;
        
        private readonly PerItemPriceCalculator _perItemPriceCalculator;
        private readonly PerKgPriceCalculator _perPerKgPriceCalculator;
        private readonly PerLiterPriceCalculator _perLiterPriceCalculator;
        
        public Order(Currency currency)
        {
            _items = new List<OrderItem>();
            
            _perItemPriceCalculator = new PerItemPriceCalculator();
            _perPerKgPriceCalculator = new PerKgPriceCalculator();
            _perLiterPriceCalculator = new PerLiterPriceCalculator();
            
            Currency = currency;
        }

        public Currency Currency { get; set; }
        public IReadOnlyList<OrderItem> Items => _items.AsReadOnly();
        
        public decimal OrderTotal_Gross => _items.Sum(x => x.SubTotal_Gross);
        public decimal OrderTotal_VatExcluded => _items.Sum(x => x.SubTotal_VAT_Excluded);
        public decimal OrderTotal_Net => _items.Sum(x => x.SubTotal_Net);
        
        public void AddItem(Item item, decimal amount)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item), "Item is invalid.");
            }

            switch (item.PriceMode)
            {
                case PriceMode.PerItem:
                    AddItem(item, amount, _perItemPriceCalculator);
                    break;
                
                case PriceMode.PerKg:
                    AddItem(item, amount, _perPerKgPriceCalculator);
                    break;
                    
                case PriceMode.PerLiter:
                    AddItem(item, amount, _perLiterPriceCalculator);
                    break;
                
                default:
                    throw new ArgumentException("Invalid PriceMode value. Value can be on of the following: PerItem, PerKg, PerLiter.");
            }
        }

        public void RemoveOrderItems(Item item)
        {
            var orderItemForRemoval = _items.Find(x => x.Item.Name == item.Name);
            if (orderItemForRemoval is null)
            {
                throw new KeyNotFoundException("No such item found in the order for removal.");
            }
            
            _items.Remove(orderItemForRemoval);
        }

        public void ClearOrderItems()
        {
            _items.Clear();
        }

        private void AddItem(Item item, decimal amount, Calculator calculator)
        {
            var existingItem = _items.Find(x => x.Item.Name == item.Name);
            if (existingItem is null)
            {
                _items.Add(new OrderItem(item, amount, Currency, calculator));
                return;
            }

            existingItem!.IncrementAmount(amount);
        }
    }
}