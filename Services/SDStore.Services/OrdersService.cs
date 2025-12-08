namespace SDStore.Services
{
    using Abstractions;
    using Data;
    using Data.Entities;
    using Shared;
    using Shared.DataTransferObjects.Request;
    using Shared.DataTransferObjects.Response;
    using Shared.DataTransferObjects.Shared;
    
    using System.Net;
    using Microsoft.EntityFrameworkCore;
    
    public class OrdersService(SDStoreDBContext store) : IOrdersService
    {
        private readonly Calculator _calculator = new();
        
        // TODO: Clear repetition and do validations
        public async Task<OperationResult<Response_OrderDetails>> GetOrder(Guid id, CancellationToken ct = default)
        {
            // TODO: Fix queries, we don't want unnecessary sql
            var order = await store.Orders
                .Include(order => order.Items)
                .ThenInclude(orderItem => orderItem.Item)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken: ct);

            if (order is not null)
            {
                return new OperationResult<Response_OrderDetails>
                {
                    Data = new Response_OrderDetails
                    {
                        Id = order.Id,
                        Email = order.Email,
                        Address = order.Address,
                        PhoneNumber = order.PhoneNumber,
                        Currency = order.Currency,
                        Total_Gross = order.Total_Gross.ToCurrency(order.Currency),
                        Total_VAT_Excluded = order.Total_VAT_Excluded.ToCurrency(order.Currency),
                        Total_Net = order.Total_Net.ToCurrency(order.Currency),
                        Items = order.Items.Select(x => new Response_OrderItem
                        {
                            Name = x.Item!.Name,
                            Amount = x.Amount.ToPriceMode(x.Item.PriceMode),
                            SubTotal_Gross = x.SubTotal_Gross.ToCurrency(x.Currency),
                            VAT_Excluded = x.VAT_Excluded.ToVATPercentage(x.VAT_Percentage),
                        })
                    },
                    StatusCode = HttpStatusCode.OK,
                };
            }

            return new OperationResult<Response_OrderDetails>
            {
                StatusCode = HttpStatusCode.NotFound,
            };
        }

        public async Task<OperationResult<IEnumerable<Response_OrderDetails>>> GetOrders(CancellationToken ct = default)
        {
            // TODO: Fix queries, we don't want unnecessary sql
            var orders = await store.Orders
                .Include(order => order.Items)
                .ThenInclude(orderItem => orderItem.Item)
                .AsNoTracking()
                .Select(order => new Response_OrderDetails()
                {
                    Id = order.Id,
                    Email = order.Email,
                    Address = order.Address,
                    PhoneNumber = order.PhoneNumber,
                    Currency = order.Currency,
                    Total_Gross = order.Total_Gross.ToCurrency(order.Currency),
                    Total_VAT_Excluded = order.Total_VAT_Excluded.ToCurrency(order.Currency),
                    Total_Net = order.Total_Net.ToCurrency(order.Currency),
                    Items = order.Items.Select(x => new Response_OrderItem
                    {
                        Name = x.Item!.Name,
                        Amount = x.Amount.ToPriceMode(x.Item.PriceMode),
                        SubTotal_Gross = x.SubTotal_Gross.ToCurrency(x.Currency),
                        VAT_Excluded = x.VAT_Excluded.ToVATPercentage(x.VAT_Percentage),
                    })
                })
                .ToListAsync(ct);

            if (orders.Count > 0)
            {
                return new OperationResult<IEnumerable<Response_OrderDetails>>
                {
                    Data = orders,
                    StatusCode = HttpStatusCode.OK,
                };
            }

            return new OperationResult<IEnumerable<Response_OrderDetails>>
            {
                Data = null,
                StatusCode = HttpStatusCode.NotFound,
            };
        }

        public async Task<OperationResult<IEnumerable<Response_OrderDetails>>> GetOrders(string email, CancellationToken ct = default)
        {
            // TODO: Fix queries, we don't want unnecessary sql
            var orders = await store.Orders
                .Where(x => x.Email == email)
                .Include(order => order.Items)
                .ThenInclude(orderItem => orderItem.Item)
                .AsNoTracking()
                .Select(order => new Response_OrderDetails()
                {
                    Id = order.Id,
                    Email = order.Email,
                    Address = order.Address,
                    PhoneNumber = order.PhoneNumber,
                    Currency = order.Currency,
                    Total_Gross = order.Total_Gross.ToCurrency(order.Currency),
                    Total_VAT_Excluded = order.Total_VAT_Excluded.ToCurrency(order.Currency),
                    Total_Net = order.Total_Net.ToCurrency(order.Currency),
                    Items = order.Items.Select(x => new Response_OrderItem
                    {
                        Name = x.Item!.Name,
                        Amount = x.Amount.ToPriceMode(x.Item.PriceMode),
                        SubTotal_Gross = x.SubTotal_Gross.ToCurrency(x.Currency),
                        VAT_Excluded = x.VAT_Excluded.ToVATPercentage(x.VAT_Percentage),
                    })
                })
                .ToListAsync(ct);

            if (orders.Count > 0)
            {
                return new OperationResult<IEnumerable<Response_OrderDetails>>
                {
                    Data = orders,
                    StatusCode = HttpStatusCode.OK,
                };
            }

            return new OperationResult<IEnumerable<Response_OrderDetails>>
            {
                Data = null,
                StatusCode = HttpStatusCode.NotFound,
            };
        }
        
        public async Task<OperationResult<IEnumerable<Response_OrderDetails>>> GetOrders(IEnumerable<Guid> ids, CancellationToken ct = default)
        {
            // TODO: Fix queries, we don't want unnecessary sql
            var orders = await store.Orders
                .Where(x => ids.Contains(x.Id))
                .Include(order => order.Items)
                .ThenInclude(orderItem => orderItem.Item)
                .AsNoTracking()
                .Select(order => new Response_OrderDetails()
                {
                    Id = order.Id,
                    Email = order.Email,
                    Address = order.Address,
                    PhoneNumber = order.PhoneNumber,
                    Currency = order.Currency,
                    Total_Gross = order.Total_Gross.ToCurrency(order.Currency),
                    Total_VAT_Excluded = order.Total_VAT_Excluded.ToCurrency(order.Currency),
                    Total_Net = order.Total_Net.ToCurrency(order.Currency),
                    Items = order.Items.Select(x => new Response_OrderItem
                    {
                        Name = x.Item!.Name,
                        Amount = x.Amount.ToPriceMode(x.Item.PriceMode),
                        SubTotal_Gross = x.SubTotal_Gross.ToCurrency(x.Currency),
                        VAT_Excluded = x.VAT_Excluded.ToVATPercentage(x.VAT_Percentage),
                    })
                })
                .ToListAsync(ct);

            if (orders.Count > 0)
            {
                return new OperationResult<IEnumerable<Response_OrderDetails>>
                {
                    Data = orders,
                    StatusCode = HttpStatusCode.OK,
                };
            }

            return new OperationResult<IEnumerable<Response_OrderDetails>>
            {
                Data = null,
                StatusCode = HttpStatusCode.NotFound,
            };
        }

        public async Task<OperationResult<Response_OrderDetails>> PlaceOrder(Request_OrderDetails order, CancellationToken ct = default)
        {
            var selectedItems = order.Items.ToDictionary(
                x => x.Id,
                x => x.Amount
            );
            
            // TODO: select only what we use
            var items = await store.Items
                .Where(x => selectedItems.Keys.Contains(x.Id))
                .ToListAsync(ct);
            
            var orderEntity = await store.Orders
                .AddAsync(new Order
                {
                    Email = order.Email,
                    Address = order.Address,
                    PhoneNumber = order.PhoneNumber,
                    Currency = order.Currency,
                }, ct);
            
            var orderItems = new List<OrderItem>();
            foreach (var item in items)
            {
                // TODO: rename Price.Amount -> Price.Price. Maybe should be Value - Price, Currency
                var subTotal_Gross = _calculator.CalculateTotalGross(
                    item.Price.Amount, selectedItems[item.Id], item.Price.Currency, order.Currency);
                var vat_Percentage = _calculator.GenerateVATPercentage();
                var vat_Excluded = _calculator.Calculate_VAT_Excluded(subTotal_Gross, vat_Percentage);
                var subTotal_Net = _calculator.Calculate_SubTotal_Net(subTotal_Gross, vat_Excluded);
            
                var orderItem = new OrderItem
                {
                    OrderId = orderEntity.Entity.Id,
                    ItemId = item.Id,
                    Amount = selectedItems[item.Id],
                    Currency = order.Currency,
                    SubTotal_Gross = subTotal_Gross,
                    VAT_Percentage = vat_Percentage,
                    VAT_Excluded = vat_Excluded,
                    SubTotal_Net = subTotal_Net,
                };
                
                orderItems.Add(orderItem);
            }
            
            orderEntity.Entity.Total_Gross = _calculator.Calculate_Total(orderItems.Select(x => x.SubTotal_Gross));
            orderEntity.Entity.Total_VAT_Excluded = _calculator.Calculate_Total(orderItems.Select(x => x.VAT_Excluded));
            orderEntity.Entity.Total_Net = _calculator.Calculate_Total(orderItems.Select(x => x.SubTotal_Net));
            
            await store.OrderItems
                .AddRangeAsync(orderItems, ct);
            
            await store.SaveChangesAsync(ct);

            return new OperationResult<Response_OrderDetails>
            {
                Data = new Response_OrderDetails
                {
                    Id = orderEntity.Entity.Id,
                    Email = orderEntity.Entity.Email,
                    Address = orderEntity.Entity.Address,
                    PhoneNumber = orderEntity.Entity.PhoneNumber,
                    Currency = orderEntity.Entity.Currency,
                    Total_Gross = orderEntity.Entity.Total_Gross.ToCurrency(orderEntity.Entity.Currency),
                    Total_VAT_Excluded = orderEntity.Entity.Total_VAT_Excluded.ToCurrency(orderEntity.Entity.Currency),
                    Total_Net = orderEntity.Entity.Total_Net.ToCurrency(orderEntity.Entity.Currency),
                    Items = orderEntity.Entity.Items.Select(x => new Response_OrderItem
                    {
                        Name = x.Item!.Name,
                        Amount = x.Amount.ToPriceMode(x.Item.PriceMode),
                        SubTotal_Gross = x.SubTotal_Gross.ToCurrency(x.Currency),
                        VAT_Excluded = x.VAT_Excluded.ToVATPercentage(x.VAT_Percentage),
                    })
                },
                StatusCode = HttpStatusCode.Created,
            };
        }
    }
}