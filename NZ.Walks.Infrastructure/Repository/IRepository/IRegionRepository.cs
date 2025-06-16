using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EG.Walks.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EG.Walks.Infrastructure.Repository.IRepository
{
    public interface IRegionRepository
    {
        // To get all regions
        Task<IEnumerable<Region>> GetAllRegionsAsync();
        // To get a specific region by ID
        Task<Region?> GetRegionByIdAsync(Guid id);
        // To Add a new region
        Task<Region> CreateRegionAsync(Region region);
        // To Update an existing region
        Task<Region?> UpdateRegionAsync(Region region);
        // To Delete a region by ID
        Task<Region?> DeleteRegionAsync(Guid id);
    }
}
