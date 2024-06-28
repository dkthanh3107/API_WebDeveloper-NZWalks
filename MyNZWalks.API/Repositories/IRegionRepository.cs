using MyNZWalks.API.Models.Domain;

namespace MyNZWalks.API.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Regions>> GetAllRegionsAsync();
        Task<Regions?> GetRegionsByIDAsync(Guid Id);
        Task<Regions> CreateRegionsAsync(Regions region);
        Task<Regions> UpdateRegionsAsync(Guid Id , Regions region);
        Task<Regions?> DeleteRegionsAsync(Guid Id);
    }
}
