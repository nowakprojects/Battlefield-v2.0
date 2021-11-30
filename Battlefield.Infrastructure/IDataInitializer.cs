namespace Battlefield.Infrastructure;

public interface IDataInitializer
{
    Task SeedAsync();
}
