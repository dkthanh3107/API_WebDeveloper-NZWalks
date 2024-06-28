using MyNZWalks.API.Models.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace MyNZWalks.API.Repositories
{
    public interface IWalksRepository
    {
        Task<List<Walks>> GetAllWalksAsync(string? filterOn = null , string? filterQuery = null , 
            string? sortBy = null , bool isAscending = true,
            int pageNumber = 1 , int pageSize = 1000);
        Task<Walks?> GetWalksByIDAsync(Guid Id);
        Task<Walks> CreateWalksAsync(Walks walks);
        Task<Walks> UpdateWalksAsync(Guid Id, Walks region);
        Task<Walks?> DeleteWalksAsync(Guid Id);
    }
}
