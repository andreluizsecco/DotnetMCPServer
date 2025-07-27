using DotnetMCPServer.Shared.Clients;
using DotnetMCPServer.Shared.Tools;
using DotnetMCPServer.StreamableHttp.Middlewares;
using ModelContextProtocol.Protocol;

var builder = WebApplication.CreateBuilder(args);

var serverInfo = new Implementation { Name = "DotnetMCPServerSSE", Version = "1.0.0" };
builder.Services
    .AddMcpServer(mcp =>
    {
        mcp.ServerInfo = serverInfo;
    })
    .WithHttpTransport()
    .WithToolsFromAssembly(typeof(LivrariaTools).Assembly);

builder.Services.AddHttpClient<LivroApiClient>(client =>
{
    var baseAddress = Environment.GetEnvironmentVariable("API_BASE_ADDRESS");
    if (!string.IsNullOrEmpty(baseAddress))
        client.BaseAddress = new Uri(baseAddress);
    else
        client.BaseAddress = new Uri("https://localhost:7294/api/");
});

var app = builder.Build();

app.UseMiddleware<ApiKeyMiddleware>();

app.MapMcp();

await app.RunAsync();