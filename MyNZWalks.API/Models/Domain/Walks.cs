﻿namespace MyNZWalks.API.Models.Domain
{
    public class Walks
    {
        
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKM { get; set; }
        public string? WalkImageURL { get; set; }
        
        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }

        //Navigation properties
        public Difficulty Difficulty { get; set; }
        public Regions Region { get; set; }
    }
}
