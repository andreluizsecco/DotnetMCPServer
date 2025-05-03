using DotnetMCPServer.Clients;
using DotnetMCPServer.DTOs;
using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Text.Json;

namespace DotnetMCPServer.Tools
{
    [McpServerToolType]
    public static class LivrariaTools
    {
        [McpServerTool, Description("Busca os livros da livraria, definindo um filtro opcional por título")]
        public static async Task<string> ObterLivros(LivroApiClient livroApiClient,
            [Description("Filtro opcional pelo título do livro")] string titulo)
        {
            try
            {
                var livros = await livroApiClient.ObterLivrosAsync(titulo);
                return livros.Count == 0
                    ? "Nenhum livro encontrado"
                    : JsonSerializer.Serialize(livros);
            }
            catch (Exception ex)
            {
                //Log
                return $"Erro ao buscar livros: {ex.Message}";
            }
        }

        [McpServerTool, Description("Busca as categorias de livros disponíveis para serem usadas no cadastro e alteração de livros")]
        public static async Task<string> ObterCategoriasDeLivros(LivroApiClient livroApiClient)
        {
            try
            {
                var categorias = await livroApiClient.ObterCategoriasAsync();
                return categorias.Count == 0
                    ? "Nenhuma categoria encontrada"
                    : JsonSerializer.Serialize(categorias);
            }
            catch (Exception ex)
            {
                //Log
                return $"Erro ao buscar categorias: {ex.Message}";
            }
        }

        [McpServerTool, Description("Busca um livro pelo código")]
        public static async Task<string> ObterLivroPorId(LivroApiClient livroApiClient,
            [Description("Filtro obrigatório pelo id")] int id)
        {
            try
            {
                var livro = await livroApiClient.ObterLivroPorIdAsync(id);
                return livro is null
                    ? "Nenhum livro encontrado"
                    : JsonSerializer.Serialize(livro);
            }
            catch (Exception ex)
            {
                //Log
                return $"Erro ao buscar livro: {ex.Message}";
            }
        }

        [McpServerTool, Description("Criar/Cadastrar um livro")]
        public static async Task<string> CadastrarLivro(LivroApiClient livroApiClient,
            [Description("Dados para criação do livro")] LivroRequest livro)
        {
            try
            {
                var id = await livroApiClient.CriarLivroAsync(livro);
                return id is null
                    ? "Não foi possível cadastrar o livro"
                    : JsonSerializer.Serialize(livro);
            }
            catch (Exception ex)
            {
                //Log
                return $"Erro ao cadastrar o livro: {ex.Message}";
            }
        }

        [McpServerTool, Description("Atualizar os dados de um livro")]
        public static async Task<string> AtualizarLivro(LivroApiClient livroApiClient,
            [Description("Código ou identificador do livro")] int id,
            [Description("Dados para atualização de um livro")] LivroRequest livro)
        {
            try
            {
                var sucesso = await livroApiClient.AtualizarLivroAsync(id, livro);
                return sucesso
                    ? "Livro atualizado com sucesso"
                    : "Não foi possível atualizar o livro";
            }
            catch (Exception ex)
            {
                //Log
                return $"Erro ao atualizar o livro: {ex.Message}";
            }
        }


        [McpServerTool, Description("Atualizar apenas o preço de um livro")]
        public static async Task<string> AtualizarPrecoDoLivro(LivroApiClient livroApiClient,
            [Description("Código ou identificador do livro")] int id,
            [Description("Novo preço do livro")] decimal preco)
        {
            try
            {
                var sucesso = await livroApiClient.AtualizarPrecoAsync(id, preco);
                return sucesso
                    ? "Preço do livro atualizado com sucesso"
                    : "Não foi possível atualizar o preço do livro";
            }
            catch (Exception ex)
            {
                //Log
                return $"Erro ao atualizar o preço do livro: {ex.Message}";
            }
        }

        [McpServerTool, Description("Excluir um livro pelo código")]
        public static async Task<string> ExcluirLivro(LivroApiClient livroApiClient,
            [Description("Filtro obrigatório pelo id")] int id)
        {
            try
            {
                var livro = await livroApiClient.ExcluirLivroAsync(id);
                return livro
                    ? "Livro excluído com sucesso"
                    : "Erro ao excluir livro";
            }
            catch (Exception ex)
            {
                //Log
                return $"Erro ao excluir livro: {ex.Message}";
            }
        }
    }
}
