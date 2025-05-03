using Livraria.Api.Entities;
using Livraria.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Livraria.Api.Controllers
{
    [ApiController]
    [Route("/api/categorias")]
    public class CategoriasController : ControllerBase
    {
        private readonly ILivroService _livroService;

        public CategoriasController(ILivroService livroService) =>
            _livroService = livroService;

        [HttpGet]
        public ActionResult<List<Categoria>> ObterCategorias()
        {
            var categorias = _livroService.ObterCategoriasDeLivros();
            if (categorias == null || categorias.Count == 0)
                return NoContent();

            return Ok(categorias);
        }
    }
}