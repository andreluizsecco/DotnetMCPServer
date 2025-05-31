using Microsoft.EntityFrameworkCore;

namespace Livraria.Api.Data.Context
{
    public class SqlServerContext : DataContext
    {
        public SqlServerContext(DbContextOptions<DataContext> options) : base(options) { }
    }
}