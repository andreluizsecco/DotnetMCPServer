using System.Net.Http.Json;
using System.Text.Json;
using DotnetMCPServer.DTOs;

namespace DotnetMCPServer.Clients
{
    public class LivroApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public LivroApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<List<LivroResponse>> ObterLivrosAsync(string? titulo = null)
        {
            var url = string.IsNullOrWhiteSpace(titulo) ? "livros" : $"livros?titulo={Uri.EscapeDataString(titulo)}";
            var response = await _httpClient.GetAsync(url);

            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                return new List<LivroResponse>();

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<LivroResponse>>(_jsonOptions);
        }

        public async Task<List<CategoriaResponse>> ObterCategoriasAsync()
        {
            var url = "categorias";
            var response = await _httpClient.GetAsync(url);

            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                return new List<CategoriaResponse>();

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<CategoriaResponse>>(_jsonOptions);
        }

        public async Task<LivroResponse?> ObterLivroPorIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"livros/{id}");

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<LivroResponse>(_jsonOptions);
        }

        public async Task<int?> CriarLivroAsync(LivroRequest livro)
        {
            var response = await _httpClient.PostAsJsonAsync("livros", livro);

            if (!response.IsSuccessStatusCode)
                return null;

            var id = await response.Content.ReadFromJsonAsync<int>(_jsonOptions);
            return id;
        }

        public async Task<bool> AtualizarLivroAsync(int id, LivroRequest livro)
        {
            var response = await _httpClient.PutAsJsonAsync($"livros/{id}", livro);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> AtualizarPrecoAsync(int id, decimal novoPreco)
        {
            var response = await _httpClient.PatchAsync($"livros/{id}/preco/{novoPreco}", null);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ExcluirLivroAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"livros/{id}");

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return false;

            response.EnsureSuccessStatusCode();
            return true;
        }
    }

}
