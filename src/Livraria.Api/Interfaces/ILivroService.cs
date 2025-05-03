using Livraria.Api.Entities;

namespace Livraria.Api.Interfaces
{
    public interface ILivroService
    {
        List<Livro> ObterLivros(string titulo);
        List<Categoria> ObterCategoriasDeLivros();
        Livro? ObterPorId(int id);
        int CriarLivro(Livro livro);
        void AtualizarLivro(Livro livroOriginal, Livro livroAlteracoes);
        void AtualizarPrecoLivro(Livro livroOriginal, decimal preco);
        void RemoverLivro(int id);
    }
}