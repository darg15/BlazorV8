using BlazorCrud.Shared;
using System.Net.Http.Json;

namespace BlazorCrud.Client.Services
{
    public class DepartamentoService : IDepartamentoService
    {
        private readonly HttpClient _http;

        public DepartamentoService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<DepartamentoDTO>> getDepartments()
         {
            var result = await _http.GetFromJsonAsync<ResponseAPI<List<DepartamentoDTO>>>("api/Departamento/getDep");

            if (result!.IsCorrect)
                return result.Value!;
            else
                throw new Exception(result.Message);
        }
    }
}
