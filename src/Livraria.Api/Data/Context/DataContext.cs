using System.Reflection;
using Livraria.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Livraria.Api.Data.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
    }
}