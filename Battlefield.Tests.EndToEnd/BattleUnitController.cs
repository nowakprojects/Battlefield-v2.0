using Battlefield.Core.Domain;
using Battlefield.Core.Domain.Creatures;
using Battlefield.Infrastructure.Commands.Battlefield;
using Battlefield.Infrastructure.Commands.BattleUnit;
using Battlefield.Infrastructure.DTO;
using Newtonsoft.Json;
using System.Net;
using Xunit;

namespace Battlefield.Tests.EndToEnd;
public class BattleUnitController : ControllerTestsBase
{
    [Fact]
    public async Task Creating_and_getting_creature_should_succeed()
    {
        //Createing Battlefield and Getting Battle Id
        var requset = new CreateBattlefiled("name1");
        var payload = GetPayLoad(requset);

        var responce = await _client.PostAsync("Battlefield", payload);

        Assert.Equal(HttpStatusCode.Created, responce.StatusCode);
        responce = await _client.GetAsync("Battlefield");
        Assert.NotNull(responce);
        var responceString = await responce.Content.ReadAsStringAsync();
        var battles = JsonConvert.DeserializeObject<IEnumerable<BattleDto>>(responceString);
        Assert.NotNull(battles);
        var battleId = battles.First().Id;

        var requset2 = new CreateUnit(battleId, 4, 6, "Archer", Player.RED);
        var payload2 = GetPayLoad(requset2);
        var ss = payload2.ReadAsStringAsync();
        var responce2 = await _client.PostAsync("BattleUnit", payload2);
        Assert.Equal(HttpStatusCode.Created, responce2.StatusCode);

        var responce3 = await _client.GetAsync($"BattleUnit/{battleId}/4/6");

        var responceString2 = await responce3.Content.ReadAsStringAsync();
        var unit = JsonConvert.DeserializeObject<BattleUnitDto>(responceString2);
        Assert.NotNull(unit);
        
    }
}
