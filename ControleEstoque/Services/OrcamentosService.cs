using System.Net.Http.Json;
using LidyDecorApp.Web.Models;

public class OrcamentosService
{
    private readonly HttpClient _httpClient;

    public OrcamentosService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<OrcamentosModel>> GetOrcamentosFromApi()
    {
        return await _httpClient.GetFromJsonAsync<List<OrcamentosModel>>("https://localhost:7071/Orcamentos");
    }

    public async Task<List<Tipoevento>> GetTiposEventoFromApi()
    {
        return await _httpClient.GetFromJsonAsync<List<Tipoevento>>("https://localhost:7071/Orcamentos/GetTiposEvento");
    }

    public async Task<HttpResponseMessage> ExcluirOrcamentos(int idOrcamentos)
    {
        return await _httpClient.DeleteAsync($"https://localhost:7071/Orcamentos/{idOrcamentos}");
    }

    public async Task<HttpResponseMessage> SalvarOrcamentos(OrcamentosModel orcamentos, bool isEditMode)
    {
        if (isEditMode)
        {
            return await _httpClient.PatchAsJsonAsync("https://localhost:7071/Orcamentos", orcamentos);
        }
        else
        {
            return await _httpClient.PostAsJsonAsync("https://localhost:7071/Orcamentos", orcamentos);
        }
    }
}
