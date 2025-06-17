using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EG.Walks.Infrastructure.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IRegionRepository Region { get; }
        // Method to save changes to the database
        IWalkRepository Walk { get; }
        Task SaveAsync();
    }
}
