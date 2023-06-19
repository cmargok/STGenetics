using System.ComponentModel.DataAnnotations;

namespace STGenetics.Application.Models.Animal
{
    public class AnimalFilterDto
    {
        public int? AnimalId { get; set; }

        [StringLength(50)]
        public string? Name { get; set; } = string.Empty;

        [StringLength(6)]
        public string? Sex { get; set; } = string.Empty;

        public bool? Status { get; set; } = null;

        public int? PageSize { get; set; } = 20;

        public int? Page { get; set; } = 1;
    }
}
