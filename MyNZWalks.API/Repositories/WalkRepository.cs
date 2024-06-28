using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MyNZWalks.API.Data;
using MyNZWalks.API.Models.Domain;
using MyNZWalks.API.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace MyNZWalks.API.Repositories
{
    public class WalkRepository : IWalksRepository
    {
        private readonly NZWalkDBContext _dbContext;
        public WalkRepository(NZWalkDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<Walks> CreateWalksAsync(Walks walks)
        {
            await _dbContext.Walks.AddAsync(walks);
            await _dbContext.SaveChangesAsync();
            return walks;
        }

        public async Task<Walks?> DeleteWalksAsync(Guid Id)
        {
            var existingWalks = await _dbContext.Walks.FirstOrDefaultAsync(x => x.Id == Id);
            if (existingWalks == null) 
            {
                return null;
            }
            _dbContext.Walks.Remove(existingWalks);
            await _dbContext.SaveChangesAsync();
            return existingWalks;
        }

        public async Task<List<Walks>> GetAllWalksAsync(string? filterOn = null, string? filterQuery = null, 
            string? sortBy = null, bool isAscending = true,
            int pageNumber = 1 , int pageSize = 1000)
        {
            var walks = _dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();

            //Filter
            if(string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if(filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
            }
            //sort By 
            if(string.IsNullOrWhiteSpace(sortBy) == false ) 
            {
                if(sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }    
                else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.LengthInKM) : walks.OrderByDescending(x => x.LengthInKM);
                }    
            }
            //Pagination
            var skip = (pageNumber - 1) * pageSize;
                
                return await walks.Skip(skip).Take(pageSize).ToListAsync();
            //return await _dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync(); 
        }

        public async Task<Walks?> GetWalksByIDAsync(Guid Id)
        {
            return await _dbContext.Walks.
                Include("Difficulty").
                Include("Region").
                FirstOrDefaultAsync(r => r.Id == Id);
        }

        public async Task<Walks> UpdateWalksAsync(Guid Id, Walks walks)
        {
            var existingWalks = await _dbContext.Walks.FirstOrDefaultAsync(r => r.Id == Id);
            if (existingWalks == null) 
                return null;
            existingWalks.Name = walks.Name;
            existingWalks.Description = walks.Description;
            existingWalks.LengthInKM = walks.LengthInKM;
            existingWalks.WalkImageURL = walks.WalkImageURL;
            existingWalks.DifficultyId = walks.DifficultyId;
            existingWalks.RegionId = walks.RegionId;
            await _dbContext.SaveChangesAsync();
            return existingWalks;
        }
    }
}
