namespace SDStore.Services
{
    using Abstractions;
    using Data;
    using Shared;
    using Shared.DataTransferObjects.Request;
    using Shared.DataTransferObjects.Response;
    using Shared.DataTransferObjects.Shared;
    
    using System.Net;
    using Microsoft.EntityFrameworkCore;
    
    public class OrdersService(SDStoreDBContext store) : IOrdersService
    {
        // TODO: Clear repetition
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
            throw new NotImplementedException();
        }
    }
}