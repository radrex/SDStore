namespace SimpleOOP.Data
{
    using System.Text.RegularExpressions;
    using SDStore.Data.Shared.Enums;
    
    public class Item
    {
        private string _name;
        private decimal _price;
        private PriceMode _priceMode;
        private Currency _currency;
        private readonly Random _random;
        
        public Item(string name, decimal price, PriceMode priceMode, Currency currency)
        {
            Name = name;
            Price = price;
            PriceMode = priceMode;
            Currency = currency;
            _random = new Random();
        }

        public string Name
        {
            get => _name;
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Name cannot be empty or whitespace");
                }

                if (value.Length <= 0 || value.Length > 150)
                {
                    throw new ArgumentException("Name has to be in the range of 1 to 150 characters.");
                }

                _name = value;
            }
        }

        public decimal Price
        {
            get => _price;
            private set
            {
                string text = value.ToString("0.00");
                if (value <= 0 || !Regex.IsMatch(text, @"^[0-9]{1,16}(?:\.[0-9]{1,2})?$"))
                {
                    throw new ArgumentException("Invalid decimal price - decimal(18,2)");
                }

                _price = value;
            }
        }

        public PriceMode PriceMode
        {
            get => _priceMode;
            private set
            {
                if (!Enum.IsDefined(typeof(PriceMode), value))
                {
                    throw new ArgumentException("Invalid PriceMode value. Value can be on of the following: PerItem, PerKg, PerLiter.");
                }

                _priceMode = value;
            }
        }
        
        public Currency Currency
        {
            get => _currency;
            private set
            {
                if (!Enum.IsDefined(typeof(Currency), value))
                {
                    throw new ArgumentException("Invalid Currency value. Value can be on of the following: BGN, EUR, USD, CAD, GBP.");
                }

                _currency = value;
            }
        }

        /// <summary>
        /// Returns a random VAT percentage between 0.00 and 27.00 (inclusive).
        /// The reason for this is that some goods/services can have different VAT rates per country.
        /// </summary>
        public decimal VAT_Percentage => _random.Next(0, 2701) / 100M;
    }
}