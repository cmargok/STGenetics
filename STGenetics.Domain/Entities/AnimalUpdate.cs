using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using STGenetics.Domain.Tools.Validations;

namespace STGenetics.Domain.Entities
{
    public class AnimalUpdate : IAnimalValidation
    {
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(50)]
        public string Breed { get; set; } = string.Empty;
      
        public DateTime BirthDate { get; set; }
       
        [StringLength(6)]
        public string Sex { get; set; } = string.Empty;
    
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        public bool? Status { get; set; } = null;
    }
}
