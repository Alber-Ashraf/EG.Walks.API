using EG.Walks.Application.Interfaces;

namespace EG.Walks.Infrastructure.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IRegionRepository Region { get; }
        IWalkRepository Walk { get; }
        ITokenRepository Token { get; }
        IImageRepository Image { get; }

        // Method to save changes to the database
        Task SaveAsync();
    }
}
