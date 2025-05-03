namespace Livraria.Api.Entities
{
    public class Livro : Entity
    {
        public string Titulo { get; private set; }
        public string Autor { get; private set; }
        public int CategoriaId { get; private set; }
        public DateTime DataPublicacao { get; private set; }
        public decimal Preco { get; private set; }

        public Categoria? Categoria { get; private set; }

        public Livro(string titulo, string autor, int categoriaId, DateTime dataPublicacao, decimal preco)
        {
            Titulo = titulo;
            Autor = autor;
            CategoriaId = categoriaId;
            DataPublicacao = dataPublicacao;
            Preco = preco;
        }

        public void AlterarDados(string titulo, string autor, int categoriaId, DateTime dataPublicacao, decimal preco)
        {
            Titulo = titulo;
            Autor = autor;
            CategoriaId = categoriaId;
            DataPublicacao = dataPublicacao;
            Preco = preco;
        }

        public void AlterarPrecoLivro(decimal preco)
        {
            Preco = preco;
        }
    }
}