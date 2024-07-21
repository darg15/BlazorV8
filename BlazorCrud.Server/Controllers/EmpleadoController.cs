using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlazorCrud.Server.Models;
using BlazorCrud.Shared;
using Microsoft.EntityFrameworkCore;


namespace BlazorCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly DbCrudBlazorContext _context;

        public EmpleadoController(DbCrudBlazorContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("getEmp")]
        public async Task<IActionResult> getEmp()
        {
            var response = new ResponseAPI<List<EmpleadoDTO>>();
            var emp = new List<EmpleadoDTO>();

            try
            {
                foreach (var i in await _context.Empleados.Include(d => d.IdDepartamentoNavigation).ToListAsync())
                {
                    emp.Add(new EmpleadoDTO
                    {

                        IdEmpleado = i.IdEmpleado,
                        NombreCompleto = i.NombreCompleto,
                        IdDepartamento = i.IdDepartamento,
                        Sueldo = i.Sueldo,
                        FechaContrato = i.FechaContrato,
                        Departamento = new DepartamentoDTO()
                        {
                            IdDepartamento = i.IdDepartamentoNavigation.IdDepartamento,
                            Nombre = i.IdDepartamentoNavigation.Nombre
                        }

                    });

                }

                response.IsCorrect = true;
                response.Value = emp;

            }
            catch (Exception ex)
            {
                response.IsCorrect = false;
                response.Message = ex.Message;
            }
            return Ok(response);
        }


        [HttpGet]
        [Route("findEmp/{id}")]
        public async Task<IActionResult> findEmp(int id )
        {
            var response = new ResponseAPI<EmpleadoDTO>();
            var emp = new EmpleadoDTO();

            try
            {
                var dbEmp = await _context.Empleados.FirstOrDefaultAsync(x => x.IdEmpleado == id);

                if(dbEmp != null)
                {
                    emp.IdEmpleado = dbEmp.IdEmpleado;
                    emp.NombreCompleto = dbEmp.NombreCompleto;
                    emp.IdDepartamento = dbEmp.IdDepartamento;
                    emp.Sueldo = dbEmp.Sueldo;
                    emp.FechaContrato = dbEmp.FechaContrato;

                    response.IsCorrect = true;
                    response.Value = emp;
                }
                else
                {
                    response.IsCorrect = false;
                    response.Message = "No encontrado";

                }

            }
            catch (Exception ex)
            {
                response.IsCorrect = false;
                response.Message = ex.Message;
            }
            return Ok(response);
        }


        [HttpPost]
        [Route("newEmp")]
        public async Task<IActionResult> newEmp(EmpleadoDTO emp)
        {
            var response = new ResponseAPI<int>();

            try
            {
                var dbEmp = new Empleado
                {
                    NombreCompleto = emp.NombreCompleto,
                    IdDepartamento= emp.IdDepartamento,
                    Sueldo = emp.Sueldo,
                    FechaContrato = emp.FechaContrato
                };

                _context.Empleados.Add(dbEmp);
                await _context.SaveChangesAsync();

                if(dbEmp.IdEmpleado != 0)
                {
                    response.IsCorrect = true;
                    response.Value = dbEmp.IdEmpleado;

                }
                else
                {
                    response.IsCorrect = false;
                    response.Message = "No se guardo";

                }

               
            }
            catch (Exception ex)
            {
                response.IsCorrect = false;
                response.Message = ex.Message;
            }
            return Ok(response);
        }


        [HttpPut]
        [Route("editEmp/{id}")]
        public async Task<IActionResult> editEmp(EmpleadoDTO emp, int id)
        {
            var response = new ResponseAPI<int>();

            try
            {
                var dbEmp = await _context.Empleados.FirstOrDefaultAsync(x => x.IdEmpleado == id);

                

                if (dbEmp != null)
                {
                    dbEmp.NombreCompleto = emp.NombreCompleto;
                    dbEmp.IdDepartamento = emp.IdDepartamento;
                    dbEmp.Sueldo = emp.Sueldo;
                    dbEmp.FechaContrato = emp.FechaContrato;

                    _context.Empleados.Update(dbEmp);
                    await _context.SaveChangesAsync();

                    response.IsCorrect = true;
                    response.Value = dbEmp.IdEmpleado;

                }
                else
                {
                    response.IsCorrect = false;
                    response.Message = "Empleado no encontrado";

                }
            }
            catch (Exception ex)
            {
                response.IsCorrect = false;
                response.Message = ex.Message;
            }
            return Ok(response);
        }


        [HttpDelete]
        [Route("delEmp/{id}")]
        public async Task<IActionResult> delEmp( int id)
        {
            var response = new ResponseAPI<int>();

            try
            {
                var dbEmp = await _context.Empleados.FirstOrDefaultAsync(x => x.IdEmpleado == id);

                if (dbEmp != null)
                {
                    _context.Empleados.Remove(dbEmp);
                    await _context.SaveChangesAsync();

                    response.IsCorrect = true;
                }
                else
                {
                    response.IsCorrect = false;
                    response.Message = "Empleado no encontrado";
                }
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
