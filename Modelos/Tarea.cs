using System.ComponentModel.DataAnnotations;

namespace Gestor_de_tareas.Modelos
{
	public class Tarea
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "El título es obligatorio.")]
		[StringLength(160, ErrorMessage = "El título no puede superar los 160 caracteres.")]
		[Display(Name = "Título")]
		public string Titulo { get; set; } = "";

		[Display(Name = "Descripción detallada")]
		public string? Descripcion { get; set; }

		[Display(Name = "Fecha de vencimiento")]
		[DataType(DataType.Date)]
		public DateTime? FechaVencimiento { get; set; }

		[Display(Name = "Usuario asignado")]
		public int? UsuarioAsignadoId { get; set; }
		public string? UsuarioAsignadoNombre { get; set; }

		[Display(Name = "Estado")]
		[Range(1, int.MaxValue, ErrorMessage = "Seleccione un estado válido.")]
		public int EstadoId { get; set; }
		public string EstadoNombre { get; set; } = "";

		[Display(Name = "Fecha de creación")]
		public DateTime CreadoEn { get; set; }

		[Display(Name = "Última actualización")]
		public DateTime ActualizadoEn { get; set; }
	}
}
