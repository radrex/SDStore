namespace SDStore.Data.Seeding
{
    using Entities;
    using Shared.Enums;
    using Shared.Structs;
    
    using Microsoft.EntityFrameworkCore;
    
    public static class DatabaseSeeder
    {
        public static void Seed(ModelBuilder builder)
        {
            SeedItems(builder);
            SeedOrders(builder);
            SeedOrderItems(builder);
        }

        private static void SeedItems(ModelBuilder builder)
        {
            builder.Entity<Item>()
                .HasData(
                    new Item { Id = 1, Name = "LRAD model Vučić", Price = new Money(10_464.95M, Currency.EUR), PriceMode = PriceMode.PerItem },
                    new Item { Id = 2, Name = "Bayraktar TB2", Price = new Money(7_000_000.00M, Currency.USD), PriceMode = PriceMode.PerItem },
                    new Item { Id = 3, Name = "F-16 Block 70", Price = new Money(150_000_000.00M, Currency.USD), PriceMode = PriceMode.PerItem },
                    new Item { Id = 4, Name = "Uaz 469 restored", Price = new Money(10_500.00M, Currency.BGN), PriceMode = PriceMode.PerItem },
                    new Item { Id = 5, Name = "Simson S50", Price = new Money(2999.00M, Currency.BGN), PriceMode = PriceMode.PerItem },
                    
                    new Item { Id = 6, Name = "Cyanide", Price = new Money(1250.00M, Currency.USD), PriceMode = PriceMode.PerKg },
                    new Item { Id = 7, Name = "Novichok Agent", Price = new Money(23_500.50M, Currency.BGN), PriceMode = PriceMode.PerKg },
                    new Item { Id = 8, Name = "Bulk 7.62x39mm Ammo (AR-M1)", Price = new Money(1755.00M, Currency.BGN), PriceMode = PriceMode.PerKg },
                    new Item { Id = 9, Name = "Ballistic Alloy Powder (Training Grade)", Price = new Money(1200.00M, Currency.CAD), PriceMode = PriceMode.PerKg },
                    new Item { Id = 10, Name = "Zimmerit (Anti‑Magnetic Armor Coating)", Price = new Money(250.50M, Currency.EUR), PriceMode = PriceMode.PerKg },
                    
                    new Item { Id = 11, Name = "Mustard Gas", Price = new Money(211.20M, Currency.EUR), PriceMode = PriceMode.PerLiter },
                    new Item { Id = 12, Name = "JP-8 Kerosene", Price = new Money(14_308.83M, Currency.USD), PriceMode = PriceMode.PerLiter },
                    new Item { Id = 13, Name = "HFD-R: Phosphoric esters (Fire Resistant)", Price = new Money(1442.10M, Currency.BGN), PriceMode = PriceMode.PerLiter },
                    new Item { Id = 14, Name = "Ricin", Price = new Money(419.30M, Currency.USD), PriceMode = PriceMode.PerLiter },
                    new Item { Id = 15, Name = "Sarin", Price = new Money(761.50M, Currency.GBP), PriceMode = PriceMode.PerLiter }
                );
        }

        private static void SeedOrderItems(ModelBuilder builder)
        {
            builder.Entity<OrderItem>()
                .HasData(
                    // todor.jivkov@prb.bg - pay in BGN - 1 Uaz (20% VAT), 2 Simson (20% VAT), 16 F-16 (15% VAT)
                    new OrderItem { OrderId = new Guid("32b00f9f-1b95-412a-8517-678c49419467"), ItemId = 4,
                        Amount = 1, Currency = Currency.BGN, SubTotal_Gross = 10_500.00M, VAT_Percentage = 20.00M,
                        VAT_Excluded = 1750.00M, SubTotal_Net = 8750.00M
                    },
                    new OrderItem { OrderId = new Guid("32b00f9f-1b95-412a-8517-678c49419467"), ItemId = 5,
                        Amount = 2, Currency = Currency.BGN, SubTotal_Gross = 5998.00M, VAT_Percentage = 20.00M,
                        VAT_Excluded = 999.67M, SubTotal_Net = 4998.33M
                    },
                    new OrderItem { OrderId = new Guid("32b00f9f-1b95-412a-8517-678c49419467"), ItemId = 3,
                        Amount = 16, Currency = Currency.BGN, SubTotal_Gross = 4_029_600_000.00M, VAT_Percentage = 15.00M,
                        VAT_Excluded = 525_600_000.00M, SubTotal_Net = 3_504_000_000.00M
                    },
                    
                    // vlad.putin@ussr.ru - pay in USD - Ricin 0.72l (9% VAT), Sarin 2.11l (9.2% VAT), Novichok Agent 0.21kg (22% VAT)
                    new OrderItem { OrderId = new Guid("c131cb5b-acb4-479b-9dea-fedb9f52aef4"), ItemId = 14,
                        Amount = 0.72M, Currency = Currency.USD, SubTotal_Gross = 301.90M, VAT_Percentage = 09.00M,
                        VAT_Excluded = 24.93M, SubTotal_Net = 276.97M
                    },
                    new OrderItem { OrderId = new Guid("c131cb5b-acb4-479b-9dea-fedb9f52aef4"), ItemId = 15,
                        Amount = 2.11M, Currency = Currency.USD, SubTotal_Gross = 2140.76M, VAT_Percentage = 09.20M,
                        VAT_Excluded = 180.36M, SubTotal_Net = 1960.40M
                    },
                    new OrderItem { OrderId = new Guid("c131cb5b-acb4-479b-9dea-fedb9f52aef4"), ItemId = 7,
                        Amount = 0.21M, Currency = Currency.USD, SubTotal_Gross = 2939.31M, VAT_Percentage = 22.00M,
                        VAT_Excluded = 530.04M, SubTotal_Net = 2409.27M
                    },
                    
                    /*
                        austrian-painter@thirdreich.de - pay in EUR
                            - Zimmerit for Tigers - 100000kg (5% VAT),
                            - Mustard Gas for trench warfare - 30000l (6.2% VAT)
                            - JP-8 Kerosene for V-2 and Messerschmitt Me 262 - 999999.99l (12.4% VAT)
                     */
                    new OrderItem { OrderId = new Guid("24a2c27a-298c-4eea-bbd9-ffa832e9a697"), ItemId = 10,
                        Amount = 10_000.00M, Currency = Currency.EUR, SubTotal_Gross = 25_050_000.00M, VAT_Percentage = 5.00M,
                        VAT_Excluded = 1_192_857.14M, SubTotal_Net = 23_857_142.86M
                    },
                    new OrderItem { OrderId = new Guid("24a2c27a-298c-4eea-bbd9-ffa832e9a697"), ItemId = 11,
                        Amount = 30_000.00M, Currency = Currency.EUR, SubTotal_Gross = 6_336_000.00M, VAT_Percentage = 6.20M,
                        VAT_Excluded = 369_898.31M, SubTotal_Net = 5_966_101.69M
                    },
                    new OrderItem { OrderId = new Guid("24a2c27a-298c-4eea-bbd9-ffa832e9a697"), ItemId = 12,
                        Amount = 999_999.99M, Currency = Currency.EUR, SubTotal_Gross = 12_283_544_750.70M, VAT_Percentage = 12.40M,
                        VAT_Excluded = 717_118_431.77M, SubTotal_Net = 11_566_426_318.93M
                    },
                    
                    /*
                        zelensky@example.ua - pay in GBP
                            - Bayraktar TB2 - 15 (10% VAT)
                            - AR-M1 Ammo - 100000kg (22% VAT)
                            - JP-8 Kerosene - 10000l (12.4% VAT)
                            - HFD-R - 30000l (15.2% VAT)
                     */
                    new OrderItem { OrderId = new Guid("920c12b3-52da-409d-833f-109f8ee0147a"), ItemId = 2,
                        Amount = 15.00M, Currency = Currency.GBP, SubTotal_Gross = 78_808_672.33M, VAT_Percentage = 10.00M,
                        VAT_Excluded = 7_164_424.76M, SubTotal_Net = 71_644_247.57M
                    },
                    new OrderItem { OrderId = new Guid("920c12b3-52da-409d-833f-109f8ee0147a"), ItemId = 8,
                        Amount = 100_000.00M, Currency = Currency.GBP, SubTotal_Gross = 78_453_285.65M, VAT_Percentage = 22.00M,
                        VAT_Excluded = 14_147_313.81M, SubTotal_Net = 64_305_971.84M
                    },
                    new OrderItem { OrderId = new Guid("920c12b3-52da-409d-833f-109f8ee0147a"), ItemId = 12,
                        Amount = 10_000.00M, Currency = Currency.GBP, SubTotal_Gross = 107_396_180.46M, VAT_Percentage = 12.40M,
                        VAT_Excluded = 11_847_977.20M, SubTotal_Net = 95_548_203.26M
                    },
                    new OrderItem { OrderId = new Guid("920c12b3-52da-409d-833f-109f8ee0147a"), ItemId = 13,
                        Amount = 30_000.00M, Currency = Currency.GBP, SubTotal_Gross = 19_339_740.72M, VAT_Percentage = 15.20M,
                        VAT_Excluded = 2_551_771.34M, SubTotal_Net = 16_787_969.38M
                    }
                );
        }
        
        private static void SeedOrders(ModelBuilder builder)
        {
            builder.Entity<Order>()
                .HasData(
                    new Order
                    {
                        Id = new Guid("32b00f9f-1b95-412a-8517-678c49419467"),
                        Email = "todor.jivkov@prb.bg", Address = "123 Maplewood Avenue, Apt 4B, Example City, CA 90001", PhoneNumber = "+1 (555) 010-0001",
                        Currency = Currency.BGN, Total_Gross = 4_029_616_498.00M, Total_VAT_Excluded = 525_602_749.67M, Total_Net = 3_504_013_748.33M
                    },
                    new Order
                    {
                        Id = new Guid("c131cb5b-acb4-479b-9dea-fedb9f52aef4"),
                        Email = "vlad.putin@ussr.ru", Address = "45 Riverside Drive, Example Town, NY 10010", PhoneNumber = "+1 (555) 010-0002",
                        Currency = Currency.USD, Total_Gross = 5381.97M, Total_VAT_Excluded = 735.33M, Total_Net = 4646.64M
                    },
                    new Order
                    {
                        Id = new Guid("24a2c27a-298c-4eea-bbd9-ffa832e9a697"),
                        Email = "austrian-painter@thirdreich.de", Address = "789 Oak Street, Suite 210, Demo City, TX 73301", PhoneNumber = "+1 (555) 010-0003",
                        Currency = Currency.EUR, Total_Gross = 12_314_930_750.70M, Total_VAT_Excluded = 718_681_187.22M, Total_Net = 11_596_249_563.48M
                    },
                    new Order
                    {
                        Id = new Guid("920c12b3-52da-409d-833f-109f8ee0147a"),
                        Email = "zelensky@example.ua", Address = "16 Pinecrest Road, Sampleville, WA 98001", PhoneNumber = "+1 (555) 010-0004",
                        Currency = Currency.GBP, Total_Gross = 283_997_879.16M, Total_VAT_Excluded = 35_711_487.11M, Total_Net = 248_286_392.05M
                    }
                );
        }
    }
}