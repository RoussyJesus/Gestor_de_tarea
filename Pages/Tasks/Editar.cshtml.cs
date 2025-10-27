using Gestor_de_tareas.Modelos;
using Gestor_de_tareas.Repositorios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Gestor_de_tareas.Pages.Tasks
{
    public class EditarModel : PageModel
    {
        private readonly RepositorioTareas _repoTareas;
        private readonly RepositorioUsuarios _repoUsuarios;

        public EditarModel(RepositorioTareas repoTareas, RepositorioUsuarios repoUsuarios)
        {
            _repoTareas = repoTareas;
            _repoUsuarios = repoUsuarios;
        }

        [BindProperty] public Tarea Tarea { get; set; } = new();
        public List<Usuario> Usuarios { get; private set; } = new();
        public List<Estado> Estados { get; private set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var encontrada = await _repoTareas.ObtenerPorIdAsync(id);
            if (encontrada is null)
                return RedirectToPage("/Index");

            Tarea = encontrada;
            Usuarios = await _repoUsuarios.ListarAsync();
            Estados = await _repoUsuarios.ListarEstadosAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Usuarios = await _repoUsuarios.ListarAsync();
            Estados = await _repoUsuarios.ListarEstadosAsync();

            if (!ModelState.IsValid)
                return Page();

            await _repoTareas.ActualizarAsync(Tarea);
            TempData["mensaje_ok"] = "Tarea actualizada correctamente.";
            return RedirectToPage("/Index");
        }
    }
}
