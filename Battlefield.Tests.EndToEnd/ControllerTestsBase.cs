using System.Net.Http;
using System.Text;
using System;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;

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
}
