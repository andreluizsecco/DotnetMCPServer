using Livraria.Api.Entities;

namespace Livraria.Api.DTOs.Request
{
    public class LivroRequest
    {
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public int CategoriaId { get; set; }
        public DateTime DataPublicacao { get; set; }
        public decimal Preco { get; set; }

        public Livro ConverterParaEntidade()
        {
            return new Livro(Titulo, Autor, CategoriaId, DataPublicacao, Preco);
        }
    }
}