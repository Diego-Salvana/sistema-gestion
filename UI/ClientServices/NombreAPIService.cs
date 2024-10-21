namespace UI.ClientServices;
public class NombreAPIService
{
    private readonly HttpClient _httpClient;

    public NombreAPIService (HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> ObtenerNombreAPI ()
    {
        return await _httpClient.GetStringAsync("") ?? "Sistema Gestión";
    }
}
