using System.ComponentModel.DataAnnotations.Schema;

namespace STGenetics.Domain.BusinessRules
{
    public class TransactionToOrder
    {
        public string ClientName { get; set; } = string.Empty;

        [Column(TypeName = "decimal(10,2)")]
        public decimal Total { get; set; }

        public DateTime PurchaseDate { get; set; }

        public List<string> Discounts { get; set; } = new();


        [Column(TypeName = "decimal(10,2)")]
        public decimal Freigth { get; set; }

        public List<Entities.Animal>? Animals { get; set; }

    }
}
