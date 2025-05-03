using Livraria.Api.DTOs.Request;
using Livraria.Api.Entities;
using Livraria.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Livraria.Api.Controllers
{
    [ApiController]
    [Route("/api/livros")]
    public class LivrosController : ControllerBase
    {
        private readonly ILivroService _livroService;

        public LivrosController(ILivroService livroService) =>
            _livroService = livroService;

        [HttpGet]
        public ActionResult<List<Livro>> ObterLivros(string? titulo)
        {
            var livros = _livroService.ObterLivros(titulo);
            if (livros == null || livros.Count == 0)
                return NoContent();

            return Ok(livros);
        }

        [HttpGet("{id}")]
        public ActionResult<Livro> ObterLivroPorId(int id)
        {
            var livro = _livroService.ObterPorId(id);
            if (livro == null)
                return NotFound();

            return Ok(livro);
        }

        [HttpPost]
        public ActionResult CriarLivro(LivroRequest livro)
        {
            var livroEntidade = livro.ConverterParaEntidade();
            var id = _livroService.CriarLivro(livroEntidade);
            return CreatedAtAction(nameof(ObterLivroPorId), new { Id = id }, id);
        }

        [HttpPut("{id}")]
        public ActionResult AtualizarLivro(int id, LivroRequest livro)
        {
            var livroOriginal = _livroService.ObterPorId(id);
            if (livroOriginal == null)
                return NotFound();

            var livroAlteracoes = livro.ConverterParaEntidade();
            _livroService.AtualizarLivro(livroOriginal, livroAlteracoes);
            return NoContent();
        }
        
        [HttpPatch("{id}/preco/{preco}")]
        public ActionResult AtualizarPrecoLivro(int id, decimal preco)
        {
            var livroOriginal = _livroService.ObterPorId(id);
            if (livroOriginal == null)
                return NotFound();

            _livroService.AtualizarPrecoLivro(livroOriginal, preco);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public ActionResult ExcluirLivro(int id)
        {
            try
            {
                _livroService.RemoverLivro(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                if (ex.ParamName.Equals("id"))
                    return NotFound();
                
                return BadRequest();
            }
            catch (Exception ex)
            {                
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensagem = ex.Message });
            }
        }
    }
}