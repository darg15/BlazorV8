using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlazorCrud.Server.Models;
using BlazorCrud.Shared;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentoController : ControllerBase
    {
        private readonly DbCrudBlazorContext _context;

        public DepartamentoController(DbCrudBlazorContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("getDep")]
        public async Task<IActionResult> getDeps() 
        {
            var response = new ResponseAPI<List<DepartamentoDTO>>();
            var deps = new List<DepartamentoDTO>();

            try
            {
                foreach(var i in await _context.Departamentos.ToListAsync())
                {
                    deps.Add(new DepartamentoDTO
                    {
                        IdDepartamento = i.IdDepartamento,
                        Nombre = i.Nombre
                    });
                }

                response.IsCorrect = true;
                response.Value = deps;
                
            }
            catch (Exception ex)
            {
                response.IsCorrect = false;
                response.Message = ex.Message;
            }
            return Ok(response);
        }
    }
}
