namespace MyNZWalks.API.Models.DTOs
{
    public class WalksDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKM { get; set; }
        public string? WalkImageURL { get; set; }
        public RegionsDTO Region { get; set; }
        public DifficultyDTO Difficulty { get; set; }
    }
}
