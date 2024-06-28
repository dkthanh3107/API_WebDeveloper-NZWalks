using Microsoft.EntityFrameworkCore;
using MyNZWalks.API.Data;
using MyNZWalks.API.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyNZWalks.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalkDBContext _dbContext;

        public RegionRepository(NZWalkDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<Regions> CreateRegionsAsync(Regions region)
        {
            await _dbContext.Regions.AddAsync(region);
            await _dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Regions> DeleteRegionsAsync(Guid id)
        {
            var exitstingRegion = await _dbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);
            if (exitstingRegion == null) 
            {
                return null;
            }
            _dbContext.Regions.Remove(exitstingRegion);
            await _dbContext.SaveChangesAsync();
            return exitstingRegion;
        }

        public async Task<List<Regions>> GetAllRegionsAsync()
        {
            return await _dbContext.Regions.ToListAsync();
        }

        public async Task<Regions?> GetRegionsByIDAsync(Guid Id)
        {
            return await _dbContext.Regions.FirstOrDefaultAsync(r => r.Id == Id);
        }

        public async Task<Regions?> UpdateRegionsAsync(Guid id, Regions region)
        {
            var existingRegion = await _dbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);
            if (existingRegion == null)
            {
                return null;
            }
            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.RegionImageURL = region.RegionImageURL;

            await _dbContext.SaveChangesAsync();
            return existingRegion;
        }
    }
}
