using System.ComponentModel.DataAnnotations;

namespace WEBAPI002.DTO{

	public class EmpleadoDto{

		public int Id { get; set; }

		[Required(ErrorMessage = "El nombre es obligatorio.")]
		public string Nombre { get; set; } = null!;

		[Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
		public DateOnly FechaNacimiento { get; set; }

		[Required(ErrorMessage = "El salario es obligatorio.")]
		public decimal Salario { get; set; }

		[Required(ErrorMessage = "La fecha de ingreso es obligatoria.")]
		public DateOnly FechaIngreso { get; set; }

		public int? JefeInmediatoId { get; set; }

		public int? AreaId { get; set; }

		public AreaDto? Area { get; set; }
	}
}