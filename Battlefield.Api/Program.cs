using Autofac;
using Autofac.Extensions.DependencyInjection;
using Battlefield.Infrastructure;
using Battlefield.Infrastructure.EventHandlers;
using Battlefield.Infrastructure.IoC;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Add services to the container
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//var timeTicker = new EventDispatcherTimeTicker(new TimeTicker(), new EventDispatcher());
var timeTicker = new TimeTicker();
builder.Services.AddSingleton<ITimeTicker>(timeTicker);
builder.Services.AddHostedService<TimeTickBackgroundService>();

// Call ConfigureContainer on the Host sub property 
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterModule(new ContainerModule());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var dataInitializer = app.Services.GetService<IDataInitializer>();
dataInitializer.SeedAsync();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

// Make the implicit Program class public so test projects can access it
public partial class Program { }
