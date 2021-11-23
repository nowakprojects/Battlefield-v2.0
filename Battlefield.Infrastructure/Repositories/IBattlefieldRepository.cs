using Battlefield.Core.Domain;

namespace Battlefield.Infrastructure.Repositories
{
    public interface IBattlefieldRepository
    {
        Task<Battle> GetAsync(Guid id);
        Task<IEnumerable<Battle>> BrowseAsync();
        Task AddAsync(Battle battle);
        Task RemoveAsync(Guid id);
    }
}
