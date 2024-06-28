using AutoMapper;
using MyNZWalks.API.Models.Domain;
using MyNZWalks.API.Models.DTOs;

namespace MyNZWalks.API.Helper
{
    public class MapperProfile : Profile
    {
        public MapperProfile() 
        {
            CreateMap<Regions, RegionsDTO>().ReverseMap();
            CreateMap<Regions, CreateRegionsRequestDTO>().ReverseMap();
            CreateMap<Regions, UpdateRegionsDTO>().ReverseMap();
            CreateMap<Walks, CreateWalksDTO>().ReverseMap();
            CreateMap<Walks, WalksDTO>().ReverseMap();
            CreateMap<Walks,UpdateWalksDTO>().ReverseMap();
            CreateMap<Difficulty, DifficultyDTO>().ReverseMap();
        }
    }
}
