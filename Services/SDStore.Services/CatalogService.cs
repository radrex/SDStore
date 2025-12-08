namespace SDStore.Services
{
    using Abstractions;
    using Data;
    using Data.Entities;
    using Shared.DataTransferObjects.Request;
    using Shared.DataTransferObjects.Response;
    using Shared.DataTransferObjects.Shared;
    
    using System.Net;
    using Microsoft.EntityFrameworkCore;
    
    public class CatalogService(SDStoreDBContext store) : ICatalogService
    {
        public async Task<OperationResult<Response_Item>> GetItem(int id, CancellationToken ct = default)
        {
            var item = await store.Items
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken: ct);

            if (item is not null)
            {
                return new OperationResult<Response_Item>
                {
                    Data = new Response_Item
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Price = item.Price,
                        PriceMode = item.PriceMode,
                    },
                    StatusCode = HttpStatusCode.OK,
                };
            }

            return new OperationResult<Response_Item>
            {
                StatusCode = HttpStatusCode.NotFound,
            };
        }

        public async Task<OperationResult<IEnumerable<Response_Item>>> GetAllItems(CancellationToken ct = default)
        {
            var items = await store.Items
                .AsNoTracking()
                .Select(x => new Response_Item
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    PriceMode = x.PriceMode,
                })
                .ToListAsync(ct);

            // TODO: Handle soft deleted items, if we even add soft delete for them
            if (items.Count > 0)
            {
                return new OperationResult<IEnumerable<Response_Item>>
                {
                    Data = items,
                    StatusCode = HttpStatusCode.OK,
                };
            }

            return new OperationResult<IEnumerable<Response_Item>>
            {
                Data = null,
                StatusCode = HttpStatusCode.NotFound,
            };
        }

        public async Task<OperationResult<Response_Item>> CreateItem(Request_ItemCreate request, CancellationToken ct = default)
        {
            var item = await store.Items.FirstOrDefaultAsync(x => x.Name == request.Name, ct);

            if (item is not null)
            {
                return new OperationResult<Response_Item>
                {
                    Data = new Response_Item
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Price = item.Price,
                        PriceMode = item.PriceMode,
                    },
                    StatusCode = HttpStatusCode.Conflict,
                };
            }

            var newItem = new Item()
            {
                Name = request.Name,
                Price = request.Price,
                PriceMode = request.PriceMode,
            };

            await store.Items.AddAsync(newItem, ct);
            await store.SaveChangesAsync(ct);

            return new OperationResult<Response_Item>
            {
                Data = new Response_Item
                {
                    Id = newItem.Id,
                    Name = newItem.Name,
                    Price = newItem.Price,
                    PriceMode = newItem.PriceMode,
                },
                StatusCode = HttpStatusCode.Created,
            };
        }

        public async Task<OperationResult<Response_Item>> UpdateItem(int id, Request_ItemUpdate request, CancellationToken ct = default)
        {
            var item = await store.Items.FirstOrDefaultAsync(x => x.Id == id, ct);

            if (item is null)
            {
                var newItem = new Item()
                {
                    Name = request.Name,
                    Price = request.Price,
                    PriceMode = request.PriceMode,
                };
                
                await store.Items.AddAsync(newItem, ct);
                await store.SaveChangesAsync(ct);

                return new OperationResult<Response_Item>
                {
                    Data = new Response_Item
                    {
                        Id = newItem.Id,
                        Name = newItem.Name,
                        Price = newItem.Price,
                        PriceMode = newItem.PriceMode,
                    },
                    StatusCode = HttpStatusCode.Created,
                };
            }

            item.Name = request.Name;
            item.Price = request.Price;
            item.PriceMode = request.PriceMode;
            await store.SaveChangesAsync(ct);
            
            return new OperationResult<Response_Item>
            {
                Data = new Response_Item
                {
                    Id = item.Id,
                    Name = item.Name,
                    Price = item.Price,
                    PriceMode = item.PriceMode,
                },
                StatusCode = HttpStatusCode.NoContent,
            };
        }
    }
}