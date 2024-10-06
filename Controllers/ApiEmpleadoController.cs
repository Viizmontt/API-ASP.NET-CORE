using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEBAPI002.Models;
using WEBAPI002.DTO;

namespace WEBAPI002.Controllers{

	[Route("api/[controller]")]
	[ApiController]
	public class ApiEmpleadoController : ControllerBase{

		private readonly Test003Context _context;

		public ApiEmpleadoController(Test003Context context){
			_context = context;
		}
		
		[HttpGet]
		public async Task<ActionResult<List<EmpleadoDto>>> GetEmpleados(){
			try{
				var empleados = await _context.Empleados.Include(e => e.Area).Select(e => new EmpleadoDto{
					Id = e.Id,
					Nombre = e.Nombre,
					FechaNacimiento = e.FechaNacimiento,
					Salario = e.Salario,
					FechaIngreso = e.FechaIngreso,
					JefeInmediatoId = e.JefeInmediatoId,
					AreaId = e.AreaId,
					Area = e.Area != null ? new AreaDto{
						Id = e.Area.Id,
						Name = e.Area.Name
					} : null
				}).ToListAsync();
				return Ok(empleados);}
			catch (Exception ex){
				return StatusCode(500, $"Error interno al traer los empleados: {ex.Message}");
			}
		}

		[HttpGet("{id}")]
		public async Task<ActionResult> GetEmpleado(int id){
			try{
				var empleado = await _context.Empleados.Where(e => e.Id == id).Select(e => new EmpleadoDto{
					Id = e.Id,
					Nombre = e.Nombre,
					FechaNacimiento = e.FechaNacimiento,
					Salario = e.Salario,
					FechaIngreso = e.FechaIngreso,
					JefeInmediatoId = e.JefeInmediatoId,
					AreaId = e.AreaId,
					Area = e.Area != null ? new AreaDto{
						Id = e.Area.Id,
						Name = e.Area.Name
					} : null
				}).FirstOrDefaultAsync();
				return Ok(empleado);
			}catch (Exception ex){
				return StatusCode(500, $"Error interno: {ex.Message}");
			}
		}
		
		[HttpPost]
		public async Task<ActionResult> PostEmpleado(EmpleadoDto empleadoDto){
			try{
				var exist = await _context.Empleados.AnyAsync(e=>e.Nombre == empleadoDto.Nombre);
				if (exist == true){
					return BadRequest("Ya existe ese empleado");
				}
				var empleado = new Empleado{
					Nombre = empleadoDto.Nombre,
					FechaNacimiento = empleadoDto.FechaNacimiento,
					Salario = empleadoDto.Salario,
					FechaIngreso = empleadoDto.FechaIngreso,
					JefeInmediatoId = empleadoDto.JefeInmediatoId,
					AreaId = empleadoDto.AreaId
				};
				_context.Empleados.Add(empleado);
				await _context.SaveChangesAsync();
				return Ok(new { message = "Empleado agregado exitosamente" });
			}catch (Exception ex){
				return StatusCode(500, $"Error interno: {ex.Message}");
			}
		}

		[HttpPut("{id}")]
		public async Task<ActionResult> PutEmpleado(int id, EmpleadoDto empleadoDto){
			try{
				var exist = await _context.Empleados.AnyAsync(e => e.Nombre == empleadoDto.Nombre && e.Id!=id);
				if (exist == true){
					return BadRequest("Ya existe ese empleado");
				}
				var empleado = await _context.Empleados.FindAsync(id);
				empleado.Nombre = empleadoDto.Nombre;
				empleado.FechaNacimiento = empleadoDto.FechaNacimiento;
				empleado.Salario = empleadoDto.Salario;
				empleado.FechaIngreso = empleadoDto.FechaIngreso;
				empleado.JefeInmediatoId = empleadoDto.JefeInmediatoId;
				empleado.AreaId = empleadoDto.AreaId;
				//_context.Entry(empleado).State = EntityState.Modified;
				await _context.SaveChangesAsync();
				return Ok(new { message = "Empleado actualizado exitosamente" });
			}catch (Exception ex){
				return StatusCode(500, $"Error interno: {ex.Message}");
			}
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> DeleteEmpleado(int id){
			try{
				var empleado = await _context.Empleados.FindAsync(id);
				var tieneSubordinados = await _context.Empleados.AnyAsync(e => e.JefeInmediatoId == id);
				if (tieneSubordinados){
					return BadRequest("No se puede eliminar el empleado porque es jefe de otros empleados");
				}
				_context.Empleados.Remove(empleado);
				await _context.SaveChangesAsync();
				return Ok(new { message = "Empleado eliminado exitosamente" });
			}catch (Exception ex){
				return StatusCode(500, $"Error interno: {ex.Message}");
			}
		}
	}
}
