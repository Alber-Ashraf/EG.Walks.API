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
        Task<IEnumerable<Walk>> GetAllWalksAsync();
        // To Get a specific walk by ID
        Task<Walk?> GetWalkByIdAsync(Guid id);
        // To Add a new region
        Task<Walk> CreateWalkAsync(Walk walk);   
    }
}
