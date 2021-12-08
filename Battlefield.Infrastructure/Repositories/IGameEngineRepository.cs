using Battlefield.Infrastructure.AI;

namespace Battlefield.Infrastructure.Repositories
{
    public interface IGameEnginesMemoryCache
    {
        Task<GameEngine> GetAsync(Guid battleGuid);
        Task AddAsync(GameEngine gameEngine);
    }

    class InMemoryGameEngineMemoryCache : IGameEnginesMemoryCache
    {
        private readonly ISet<GameEngine> _cache = new HashSet<GameEngine>();
        
        public async Task<GameEngine> GetAsync(Guid battleGuid)
        {
            var battle = _cache.SingleOrDefault(x => battleGuid.Equals(x.BattleGuid()));
            if (battle == null)
            {
                throw new Exception($"There is no GameEngine with id: {battleGuid}.");
            }
            await Task.CompletedTask;
            return battle;
        }

        public Task AddAsync(GameEngine gameEngine)
        {
            _cache.Add(gameEngine);
            return Task.CompletedTask;
        }
    }
}
