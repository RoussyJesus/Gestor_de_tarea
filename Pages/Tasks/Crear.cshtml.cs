using Gestor_de_tareas.Modelos;
using Gestor_de_tareas.Repositorios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Gestor_de_tareas.Pages.Tasks
{
    public class CrearModel : PageModel
    {
        private readonly RepositorioTareas _repoTareas;
        private readonly RepositorioUsuarios _repoUsuarios;

        public CrearModel(RepositorioTareas repoTareas, RepositorioUsuarios repoUsuarios)
        {
            _repoTareas = repoTareas;
            _repoUsuarios = repoUsuarios;
        }

        [BindProperty] public Tarea Tarea { get; set; } = new();
        public List<Usuario> Usuarios { get; private set; } = new();
        public List<Estado> Estados { get; private set; } = new();

        public async Task OnGet()
        {
            Usuarios = await _repoUsuarios.ListarAsync();
            Estados = await _repoUsuarios.ListarEstadosAsync();
            Tarea.EstadoId = Estados.FirstOrDefault()?.Id ?? 1; // Pendiente por defecto
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Usuarios = await _repoUsuarios.ListarAsync();
            Estados = await _repoUsuarios.ListarEstadosAsync();

            if (!ModelState.IsValid)
                return Page();

            var nuevoId = await _repoTareas.CrearAsync(Tarea);
            return RedirectToPage("/Index", new { creado = nuevoId });
        }
    }
}