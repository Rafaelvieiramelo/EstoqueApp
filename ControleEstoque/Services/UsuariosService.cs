using System.Net.Http.Json;
using LidyDecorApp.Web.Models;

public class UsuariosService
{
    private readonly HttpClient _httpClient;

    public UsuariosService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<UsuariosModel>> GetUsuariosFromApi()
    {
        return await _httpClient.GetFromJsonAsync<List<UsuariosModel>>("https://localhost:7071/Usuarios");
    }

    public async Task<HttpResponseMessage> ExcluirUsuarios(int idUsuarios)
    {
        return await _httpClient.DeleteAsync($"https://localhost:7071/Usuarios/{idUsuarios}");
    }

    public async Task<HttpResponseMessage> SalvarUsuarios(UsuariosModel usuarios, bool isEditMode)
    {
        if (isEditMode)
        {
            return await _httpClient.PatchAsJsonAsync("https://localhost:7071/Usuarios", usuarios);
        }
        else
        {
            return await _httpClient.PostAsJsonAsync("https://localhost:7071/Usuarios", usuarios);
        }
    }
}
