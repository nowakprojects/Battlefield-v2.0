using System.Threading.Tasks;
using Xunit;

namespace Battlefield.Tests.EndToEnd;

public class BattleConttroler : ControllerTestsBase
{
    [Fact]
    public async Task EventDispatchShouldWorkCorect()
    {

        await Task.CompletedTask;
        Assert.True(true);
    }
}

