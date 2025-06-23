namespace EG.Walks.UI.Models.DTOs
{
    public class WalkResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        public DifficultyDto Difficulty { get; set; }
        public RegionDto Region { get; set; }
    }
}
