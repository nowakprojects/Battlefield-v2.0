using System.Net.Http;
using System.Text;
using System;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Battlefield.Infrastructure.DTO;

namespace Battlefield.Tests.EndToEnd;

public abstract class ControllerTestsBase
{

    protected readonly WebApplicationFactory<Program> factory;

    protected readonly HttpClient _client;
    public ControllerTestsBase()
    {
        factory = new WebApplicationFactory<Program>();
        _client = factory.CreateClient();
    }

    protected static StringContent GetPayLoad(object data)
    {
        var json = JsonConvert.SerializeObject(data);
        return new StringContent(json, Encoding.UTF8, "application/json");
    }
    protected async Task<BattleDto?> GetBattleAsync(Guid battleId)
    {
        var responce = await _client.GetAsync($"battlefield/{battleId}");
        var responceString = await responce.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<BattleDto>(responceString);

    }
}
