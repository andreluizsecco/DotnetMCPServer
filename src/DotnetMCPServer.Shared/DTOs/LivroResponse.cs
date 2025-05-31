namespace DotnetMCPServer.Shared.DTOs
{
    public class LivroResponse
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public DateTime DataPublicacao { get; set; }
        public decimal Preco { get; set; }
        public CategoriaResponse Categoria { get; set; } = new CategoriaResponse();
    }
}
