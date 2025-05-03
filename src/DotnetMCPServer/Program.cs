using DotnetMCPServer.Clients;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Protocol.Types;

var builder = Host.CreateApplicationBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole(options =>
{
    options.LogToStandardErrorThreshold = LogLevel.Debug;
});

builder.Configuration.AddEnvironmentVariables();

var serverInfo = new Implementation { Name = "DotnetMCPServer", Version = "1.0.0" };
builder.Services
    .AddMcpServer(mcp =>
    {
        mcp.ServerInfo = serverInfo;
    })
    .WithStdioServerTransport()
    .WithToolsFromAssembly();

builder.Services.AddHttpClient<LivroApiClient>(client =>
{
    var baseAddress = Environment.GetEnvironmentVariable("API_BASE_ADDRESS");
    if (!string.IsNullOrEmpty(baseAddress))
        client.BaseAddress = new Uri(baseAddress);
    else
        client.BaseAddress = new Uri("https://localhost:7294/api/");
});

var app = builder.Build();
await app.RunAsync();