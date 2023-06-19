using System.ComponentModel.DataAnnotations.Schema;

namespace STGenetics.Domain.Tools.Validations
{
    public interface IAnimalValidation
    {
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        public DateTime BirthDate { get; set; }
    }
}
