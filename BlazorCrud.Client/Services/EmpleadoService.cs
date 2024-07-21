using BlazorCrud.Shared;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace BlazorCrud.Client.Services
{
    public class EmpleadoService : IEmpleadoService
    {

        private readonly HttpClient _http;

        public EmpleadoService(HttpClient httpClient) { 
            _http = httpClient;
        }


        public async Task<List<EmpleadoDTO>> getEmps()
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<List<EmpleadoDTO>>>("api/Empleado/getEmp");

            if (result!.IsCorrect)
                return result.Value!;
            else
                throw new Exception(result.Message);
        }


        public async Task<EmpleadoDTO> findEmp(int id)
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<EmpleadoDTO>>($"api/Empleado/findEmp/{id}");

            if (result!.IsCorrect)
                return result.Value!;
            else
                throw new Exception(result.Message);
        }


        public async Task<int> newEmp(EmpleadoDTO emp)
        {
            var result = await _http.PostAsJsonAsync("api/Empleado/newEmp", emp);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.IsCorrect)
                return response.Value!;
            else
                throw new Exception(response.Message);
        }

        public async Task<int> editEmp(EmpleadoDTO emp)
        {
            var result = await _http.PutAsJsonAsync($"api/Empleado/editEmp/{emp.IdEmpleado}", emp);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.IsCorrect)
                return response.Value!;
            else
                throw new Exception(response.Message);
        }


        public async Task<bool> delDep(int id)
        {
            var result = await _http.DeleteAsync($"api/Empleado/delEmp/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.IsCorrect)
                return response.IsCorrect!;
            else
                throw new Exception(response.Message);
        }

       
      

       

      
    }
}
