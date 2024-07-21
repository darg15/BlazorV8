using BlazorCrud.Shared;

namespace BlazorCrud.Client.Services
{
    public interface IEmpleadoService
    {
        Task<List<EmpleadoDTO>> getEmps();
        Task<EmpleadoDTO> findEmp(int id);
        Task<int> newEmp(EmpleadoDTO emp);
        Task<int> editEmp(EmpleadoDTO emp);
        Task<bool> delDep(int id);
    }
}
