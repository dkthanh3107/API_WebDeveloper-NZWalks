﻿namespace MyNZWalks.API.Models.DTOs
{
    public class RegionsDTO
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string? RegionImageURL { get; set; }
    }
}
