using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STGenetics.Application.Tools.ValidationAttributes;
using STGenetics.Domain.Tools.Validations;

namespace STGenetics.Application.Models.Animal
{
    public class AnimalDto : IAnimalValidation
    {        
        public int AnimalId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Breed { get; set; } = string.Empty;

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        [StringLength(6)]
        public string Sex { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        [Required]
        public bool Status { get; set; } = false;
    }

    
}
