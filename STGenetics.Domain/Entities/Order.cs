using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace STGenetics.Domain.Entities
{
    public class Order
    {
        public int OrderId { get; set; }

        [Required]
        [StringLength(50)]
        public string ClientName { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Total { get; set; }

        [Required]
        public DateTime PurchaseDate { get; set; }

        [Required]
        [StringLength(500)]
        public string DiscountsApplied { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Freight { get; set; }

        public bool Paid { get; set; } = false;


    }
}
