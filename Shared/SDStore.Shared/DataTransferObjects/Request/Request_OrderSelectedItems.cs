namespace SDStore.Shared.DataTransferObjects.Request
{
    using System.ComponentModel.DataAnnotations;
    
    public record Request_OrderSelectedItems
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Id must be between 1 and the maximum allowed integer.")]
        public int Id { get; init; }
        
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Amount must have at most 2 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Amount should be in 0 - 9999999999999999.99 range.")]
        public decimal Amount { get; init; }
    }
}