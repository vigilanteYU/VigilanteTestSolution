using Microsoft.Extensions.DependencyInjection;
using MongoDBTest;
using Microsoft.Extensions.Configuration.UserSecrets;

var builder = ConsoleApp.CreateBuilder(args);

builder.ConfigureServices((ctx, services) =>
{
    services.Configure<MongoDbConfig>(ctx.Configuration.GetSection("MongoDbConfig"));
});

var app = builder.Build();

app.AddCommands<MongoDbApp>();
app.Run();