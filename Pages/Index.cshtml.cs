using Gestor_de_tareas.Modelos;
using Gestor_de_tareas.Repositorios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Gestor_de_tareas.Pages
{
	public class IndexModel : PageModel
	{
        private readonly RepositorioTareas _repoTareas;
        private readonly RepositorioUsuarios _repoUsuarios;

        public IndexModel(RepositorioTareas repoTareas, RepositorioUsuarios repoUsuarios)
        {
            _repoTareas = repoTareas;
            _repoUsuarios = repoUsuarios;
        }

        [BindProperty(SupportsGet = true)]
        public int? FiltroEstadoId { get; set; }

        public List<Tarea> Tareas { get; private set; } = new();
        public List<Estado> Estados { get; private set; } = new();

        public async Task OnGet()
        {
            Estados = await _repoUsuarios.ListarEstadosAsync();
            Tareas = await _repoTareas.ListarAsync(FiltroEstadoId);
        }

        public async Task<IActionResult> OnPostCambiarEstadoAsync([FromBody] CambiarEstadoDto datos)
        {
            if (datos is null || datos.Id <= 0) return BadRequest("Solicitud inválida.");
            var result = await _repoTareas.CambiarEstadoAsync(datos.Id, datos.NuevoEstadoId);
            return new JsonResult(new { id = datos.Id, estadoId = result.EstadoId, estadoNombre = result.EstadoNombre });
        }

        

        public record CambiarEstadoDto(int Id, int NuevoEstadoId);
       
        public async Task<IActionResult> OnPostEliminarAsync(int id)
        {
            if (id <= 0)
                return BadRequest("Id inválido");

            try
            {
                await _repoTareas.EliminarAsync(id);
                return new JsonResult(new { ok = true, id });
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return new JsonResult(new { ok = false, error = ex.Message });
            }
        }

    }
}
