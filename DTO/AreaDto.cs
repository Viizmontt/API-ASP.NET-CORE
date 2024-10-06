using System.ComponentModel.DataAnnotations;

namespace WEBAPI002.DTO{

	public class AreaDto{

		public int Id { get; set; }

		[Required(ErrorMessage = "El nombre es obligatorio.")]
		public string Name { get; set; } = null!;

	}
}