using System.ComponentModel.DataAnnotations;

namespace Gestor_de_tareas.Modelos
{
    /****/
    public class Usuario
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Los nombres son obligatorios.")]
		[StringLength(120)]
		[Display(Name = "Nombres completos")]
		public string Nombres { get; set; } = "";

		[Required(ErrorMessage = "El correo es obligatorio.")]
		[EmailAddress(ErrorMessage = "Debe ingresar un correo válido.")]
		[Display(Name = "Correo electrónico")]
		public string Correo { get; set; } = "";
	}
}
