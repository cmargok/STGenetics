namespace STGenetics.Application.Models.Animal
{
    public class AnimalsFilteredDto
    {
        public int Quantity { get; set; }
        public int Page { get; set; }

        public List<AnimalDto> Animals { get; set; } = new();
    }
}
