using Livraria.Api.Data.Context;
using Livraria.Api.Entities;
using Livraria.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Livraria.Api.Services
{
    public class LivroService : ILivroService
    {
        private readonly DataContext _context;

        public LivroService(DataContext context)
        {
            _context = context;
        }

        public List<Livro> ObterLivros(string titulo)
        {
            return _context.Livros
                .Include(p => p.Categoria)
                .Where(p => string.IsNullOrWhiteSpace(titulo) || p.Titulo.Contains(titulo))
                .ToList();
        }

        public List<Categoria> ObterCategoriasDeLivros()
        {
            return _context.Categorias
                .ToList();
        }

        public Livro? ObterPorId(int id)
        {
            return _context.Livros
                .Include(p => p.Categoria)
                .FirstOrDefault(p => p.Id == id);
        }

        public int CriarLivro(Livro livro)
        {
            _context.Livros.Add(livro);
            _context.SaveChanges();
            return livro.Id;
        }

        public void AtualizarLivro(Livro livroOriginal, Livro livroAlteracoes)
        {
            livroOriginal.AlterarDados(livroAlteracoes.Titulo,
                                       livroAlteracoes.Autor,
                                       livroAlteracoes.CategoriaId,
                                       livroAlteracoes.DataPublicacao,
                                       livroAlteracoes.Preco);
            _context.SaveChanges();
        }

        public void AtualizarPrecoLivro(Livro livroOriginal, decimal preco)
        {
            livroOriginal.AlterarPrecoLivro(preco);
            _context.SaveChanges();
        }

        public void RemoverLivro(int id)
        {
            var livro = ObterPorId(id);
            if (livro == null)
                throw new ArgumentException("O livro com o identificador informado não existe", "id");

            _context.Livros.Remove(livro);
            _context.SaveChanges();
        }
    }
}