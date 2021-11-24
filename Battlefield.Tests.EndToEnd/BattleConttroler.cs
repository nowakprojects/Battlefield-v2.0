using Battlefield.Core.Domain;
using Battlefield.Infrastructure.Commands.Battlefield;
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
    public async Task test()
    {
        var requset = new CreateBattlefiled("name");
        var payload = GetPayLoad(requset);

        var responce = await _client.PostAsync("Battle", payload);

        Assert.Equal(HttpStatusCode.Created, responce.StatusCode);

        responce = await _client.GetAsync("Battle");
        Assert.NotNull(responce);
        var str = await responce.Content.ReadAsStringAsync();
        Assert.Equal("Id = ", str);
    }
}

