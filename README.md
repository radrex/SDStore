# SDStore Assignment

Please create a small ordering application or set of classes having:
- items catalog;
- each item in the catalog is defined as {name, price, priceMode};
- priceMode could be one of the following: "perItem", "perKg", "perLiter";
- "order" class allowing items to be added/removed and order totals (subtotal, VAT, totals) be calculated;
- support for multiple currencies, where catalog prices are stored in a base currency and converted to the order currency using an exchange rate at the time of adding the item.

# Solution 1: Building a controller based Web Api with Scalar, EF Core, MSSQL

Dev Process thoughts:
- Items - OrderItems - Order
  - Many-to-many with mapping table because we will need to calculate SubTotal and store Quantity.
  - Issue 1:
    - If we have a PriceMode == PerItem, we are all good and Quantity:int field will do the job.
    - However if we have PriceMode == PerKg, Quantity:int field doesn't work and the same goes for PerLiter
    - So I guess I will use decimal(18,2). This way we can do the right calculations.
    - This also means the name "Quantity" doesn't fit. "Amount" it is.
    - We will also need validation for when PriceMode == PerItem, cuz we don't want the value to be 1.20 for example.
  - Issue 2:
    - Storing the Item price in a format that would allow for conversion based on exchange rates and Currency. Used the following:
      - https://learn.microsoft.com/en-us/ef/core/modeling/value-conversions?tabs=data-annotations#composite-value-objects
      - After coding this, I think it was redundant but whatever... I could've just assumed the Price is in BGN from the start.

TODOs for enhancement of the Web API:
- Better GlobalErrorHandling implementation. Let's not rely on ApiController to handle 400 responses
- Validation - maybe we can extract the repeating validation attributes into a custom validation attribute for all the models that use the same validations

- PROCESSING:
  - Paging
  - Filtering
  - Searching
  - Sorting(Ordering)
  - Data Shaping
  - HATEOAS
  - Versioning
  - Caching (maybe Output Caching cuz its cooler than Response Caching)
  - Cache Revalidation (ETag <3)
  - Rate Limiting and Throttling
  - Supporting formatters - same endpoint multiple formats based on the "Accept" header (application/json, text/xml, etc.)
- Middlewares and Filters ?
- Logging? Serilog?

# Solution 2: Use JSON as a DB or something? 
- So we can have an Order class with methods: Add, Remove, Total instead of api endpoints ?
- Maybe play around with inheritance, polymorphism, encapsulation, command pattern (for Add, Remove, Total) ?
