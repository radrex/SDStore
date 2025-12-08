namespace SDStore.Data.Entities
{
    using Shared.Enums;
    
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public class Order
    {
        #region PROPERTIES
        
        [Key]
        public Guid Id { get; set; }
        
        /// <summary>
        /// As per RFC3696 - https://www.rfc-editor.org/rfc/rfc3696
        /// 
        /// In addition to restrictions on syntax, there is a length limit on email addresses.
        /// That limit is a maximum of 64 characters (octets) in the "local part" (before the "@")
        /// and a maximum of 255 characters (octets) in the domain part (after the "@") for a total length of 320
        /// characters. Systems that handle email should be prepared to process addresses which are that long,
        /// even though they are rarely encountered.
        /// </summary>
        [MaxLength(320)]
        [EmailAddress] // This attribute implementation is retarded. For example .@. is valid ¯\_(ツ)_/¯
        public required string Email { get; set; }
        
        /*
            https://en.wikipedia.org/wiki/List_of_long_place_names - Try reading the 1st one lol :D
            https://www.youtube.com/watch?v=DUGN-12HHwQ
         */
        [Required]
        [MaxLength(200)]
        public required string Address { get; set; }
        
        // Taken from: https://ihateregex.io/expr/phone/
        [RegularExpression(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$")]
        public required string PhoneNumber { get; set; }

        public Currency Currency { get; set; }
        
        /// <summary>
        /// Sum of all <see cref="OrderItem.SubTotal_Gross"/> Items.
        /// </summary>
        [Column(TypeName = "decimal(18,2)")]
        public decimal Total_Gross { get; set; }
        
        /// <summary>
        /// Sum of all <see cref="OrderItem.VAT_Excluded"/> Items.
        /// </summary>
        [Column(TypeName = "decimal(18,2)")]
        public decimal Total_VAT_Excluded { get; set; }
        
        /// <summary>
        /// Sum of all <see cref="OrderItem.SubTotal_Net"/> Items.
        /// </summary>
        [Column(TypeName = "decimal(18,2)")]
        public decimal Total_Net { get; set; }
        
        #endregion

        #region INVERSE FOREIGN KEY PROPERTIES [FK]
        
        public virtual ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();

        #endregion
    }
}