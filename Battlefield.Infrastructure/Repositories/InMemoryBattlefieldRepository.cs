using Battlefield.Core.Domain;

namespace Battlefield.Infrastructure.Repositories
{
    public class InMemoryBattlefieldRepository : IBattlefieldRepository
    {
        private readonly ISet<Battle> _battles = new HashSet<Battle>();
        public async Task<Battle?> GetAsync(string name)
        {
            var battle = _battles.SingleOrDefault(x => name == x.Name);
            await Task.CompletedTask;
            return battle;
        }
        public async Task<Battle> GetAsync(Guid id)
        {
            var battle = _battles.SingleOrDefault(x => id == x.Id);
            if (battle == null)
            {
                throw new Exception($"There is no Battle with id: {id}.");
            }
            await Task.CompletedTask;
            return battle;
        }
       
        public async Task AddAsync(Battle battle)
        {
            _battles.Add(battle);
            await Task.CompletedTask;
        }
        public async Task<IEnumerable<Battle>> BrowseAsync()
            => await Task.FromResult(_battles);
        public async Task RemoveAsync(Guid id)
        {
            var battle = await GetAsync(id);
            _battles.Remove(battle);
            await Task.CompletedTask;
        }

    }
}
