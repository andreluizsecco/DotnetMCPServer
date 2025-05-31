using Livraria.Api.Data.Context;
using Livraria.Api.Interfaces;
using Livraria.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var provider = builder.Configuration["DatabaseProvider"];

builder.Services.AddDbContext<DataContext>(options =>
{
    var conn = builder.Configuration.GetConnectionString(provider);

    if (provider == "SqlServer")
        options.UseSqlServer(conn);
    else if (provider == "Postgres")
        options.UseNpgsql(conn);
    else
        throw new InvalidOperationException("DatabaseProvider inválido");
});
builder.Services.AddScoped<ILivroService, LivroService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
