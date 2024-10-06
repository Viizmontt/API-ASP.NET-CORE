using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEBAPI002.Models;

namespace WEBAPI002.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AreaController : ControllerBase
	{

		private readonly Test003Context _context;

		public AreaController(Test003Context context){
			_context = context;
		}

		[HttpGet]
		public async Task<ActionResult<List<Area>>> GetAreas(){
			try{
				var areas = await _context.Areas.ToListAsync();
				return Ok(areas);
			}
			catch (Exception ex){

				return StatusCode(500, $"Error interno al cargar las Areas: {ex.Message}");
			}
		}

		[HttpGet("{id}")]
		public async Task<ActionResult> GetAreaById(int id){
			try{
				var area = await _context.Areas.FindAsync(id);
				return Ok(area);
			}
			catch (Exception ex){
				return StatusCode(500, $"Error interno al cargar el Area: {ex.Message}");
			}
		}

		[HttpPost]
		public async Task<ActionResult<Area>> PostArea(Area area){
			try{
				var existingArea = await _context.Areas.AnyAsync(a => a.Name == area.Name);
				if (existingArea == true){
					return BadRequest("Ya existe esa area");
				}
				_context.Areas.Add(area);
				await _context.SaveChangesAsync();
				return Ok(new {message="Area creada"});
			}
			catch (Exception ex){
				return StatusCode(500, $"Error interno al ingresar el area: {ex.Message}");
			}
		}

		[HttpPut("{id}")]
		public async Task<ActionResult> PutArea(int id, Area area){
			try{
				var existingArea = await _context.Areas.AnyAsync(a => a.Name == area.Name && a.Id != id);
				if (existingArea == true){
					return BadRequest("Ya existe esa area");
				}
				_context.Entry(area).State = EntityState.Modified;
				await _context.SaveChangesAsync();
				return Ok(new { message = "Area modificada" });
			}
			catch (Exception ex){
				return StatusCode(500, $"Error interno al actualizar el area: {ex.Message}");
			}
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> DeleteArea(int id){
			try{
				var area = await _context.Areas.FindAsync(id);
				_context.Areas.Remove(area);
				await _context.SaveChangesAsync();
				return Ok(new { message = "Area eliminada" });
			}
			catch (Exception ex){
				return StatusCode(500, $"Error interno al eliminar el area: {ex.Message}");
			}
		}
	}
}
