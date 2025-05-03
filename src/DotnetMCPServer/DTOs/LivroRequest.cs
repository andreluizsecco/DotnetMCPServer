namespace DotnetMCPServer.DTOs
{
    public class LivroRequest
    {
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public int CategoriaId { get; set; }
        public DateTime DataPublicacao { get; set; }
        public decimal Preco { get; set; }
    }
}
