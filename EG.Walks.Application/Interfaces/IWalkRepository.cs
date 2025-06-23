using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EG.Walks.Domain.Entities;

namespace EG.Walks.Infrastructure.Repository.IRepository
{
    public interface IWalkRepository
    {
        // To Get All Walks
        Task<IEnumerable<Walk>> GetAllWalksAsync(string? filterOn = null, string? filterQuery = null
            , string? sortBy = null, bool isAscending = true
            , int page = 1, int pageSize = 10);
        // To Get a specific walk by ID
        Task<Walk?> GetWalkByIdAsync(Guid id);
        // To Add a new region
        Task<Walk> CreateWalkAsync(Walk walk);
        // To Update an existing walk
        Task<Walk?> UpdateWalkAsync(Guid id, Walk walk);
        // To Delete a walk by ID
        Task<Walk?> DeleteWalkAsync(Guid id);
    }
}
