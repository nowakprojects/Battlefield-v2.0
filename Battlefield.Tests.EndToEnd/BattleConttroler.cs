using Battlefield.Core.Domain;
using Battlefield.Infrastructure.Commands.Battlefield;
using Battlefield.Infrastructure.DTO;
using Newtonsoft.Json;
using System.Net;
using Xunit;

namespace Battlefield.Tests.EndToEnd;

public class BattleConttroler : ControllerTestsBase
{
    [Fact]
    public async Task Creating_Battlefield_Should_Succeed()
    {
        var requset = new CreateBattlefiled("name");
        var payload = GetPayLoad(requset);

        var responce = await _client.PostAsync("Battlefield", payload);

        Assert.Equal(HttpStatusCode.Created, responce.StatusCode);
        
    }
    [Fact]
    public async Task test()
    {
        var requset = new CreateBattlefiled("name");
        var payload = GetPayLoad(requset);

        var responce = await _client.PostAsync("Battlefield", payload);

        Assert.Equal(HttpStatusCode.Created, responce.StatusCode);

        responce = await _client.GetAsync("Battlefield");
        Assert.NotNull(responce);
        var responceString = await responce.Content.ReadAsStringAsync();
        var battles = JsonConvert.DeserializeObject<IEnumerable<BattleDto>>(responceString);
        Assert.NotNull(battles);
        var battleId = battles.First().Id;
        var battle = await GetBattleAsync(battleId);
        Assert.Equal(battleId, battle.Id);
    }
}

