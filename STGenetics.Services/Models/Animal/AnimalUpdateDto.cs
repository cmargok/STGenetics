using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace STGenetics.Application.Models.Animal
{
    public class AnimalUpdateDto
    {
        [StringLength(50)]
        public string? Name { get; set; } = string.Empty;

        [StringLength(50)]
        public string? Breed { get; set; } = string.Empty;

        public DateTime? BirthDate { get; set; }

        [StringLength(6)]
        public string Sex { get; set; } = string.Empty;


        [Column(TypeName = "decimal(10,2)")]
        public decimal? Price { get; set; }

        public bool? Status { get; set; } = null;
    }
}
