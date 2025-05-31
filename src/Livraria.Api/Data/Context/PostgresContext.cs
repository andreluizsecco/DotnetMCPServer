using Microsoft.EntityFrameworkCore;

namespace Livraria.Api.Data.Context
{
    public class PostgresContext : DataContext
    {
        public PostgresContext(DbContextOptions<DataContext> options) : base(options) { }
    }
}