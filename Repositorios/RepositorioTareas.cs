using Gestor_de_tareas.Datos;
using Gestor_de_tareas.Modelos;
using Microsoft.Data.SqlClient;

namespace Gestor_de_tareas.Repositorios
{
	public class RepositorioTareas
	{
		private readonly ConexionBd _bd;
		public RepositorioTareas(ConexionBd bd) => _bd = bd;

		public Task<List<Tarea>> ListarAsync(int? estadoId = null) =>
			_bd.LeerListaAsync("dbo.spTareas_Listar", r => new Tarea
			{
				Id = (int)r["Id"],
				Titulo = (string)r["Titulo"],
				Descripcion = r["Descripcion"] as string,
				FechaVencimiento = r["FechaVencimiento"] as DateTime?,
				UsuarioAsignadoId = r["UsuarioAsignadoId"] as int?,
				UsuarioAsignadoNombre = r["UsuarioAsignadoNombre"] as string,
				EstadoId = (int)r["EstadoId"],
				EstadoNombre = (string)r["EstadoNombre"],
				CreadoEn = (DateTime)r["CreadoEn"],
				ActualizadoEn = (DateTime)r["ActualizadoEn"]
			},
			cmd =>
			{
				cmd.Parameters.Add(new SqlParameter("@EstadoId", (object?)estadoId ?? DBNull.Value));
			});

		public Task<Tarea?> ObtenerPorIdAsync(int id) =>
			_bd.LeerUnoAsync("dbo.spTareas_ObtenerPorId", r => new Tarea
			{
				Id = (int)r["Id"],
				Titulo = (string)r["Titulo"],
				Descripcion = r["Descripcion"] as string,
				FechaVencimiento = r["FechaVencimiento"] as DateTime?,
				UsuarioAsignadoId = r["UsuarioAsignadoId"] as int?,
				EstadoId = (int)r["EstadoId"]
			},
			cmd => cmd.Parameters.Add(new SqlParameter("@Id", id)));

		public async Task<int> CrearAsync(Tarea tarea)
		{
			var nuevoId = await _bd.EscalarAsync("dbo.spTareas_Crear", cmd =>
			{
				cmd.Parameters.Add(new SqlParameter("@Titulo", tarea.Titulo));
				cmd.Parameters.Add(new SqlParameter("@Descripcion", (object?)tarea.Descripcion ?? DBNull.Value));
				cmd.Parameters.Add(new SqlParameter("@FechaVencimiento", (object?)tarea.FechaVencimiento ?? DBNull.Value));
				cmd.Parameters.Add(new SqlParameter("@UsuarioAsignadoId", (object?)tarea.UsuarioAsignadoId ?? DBNull.Value));
				cmd.Parameters.Add(new SqlParameter("@EstadoId", tarea.EstadoId));
			});
			return Convert.ToInt32(nuevoId);
		}

		public Task ActualizarAsync(Tarea tarea) =>
			_bd.EjecutarAsync("dbo.spTareas_Actualizar", cmd =>
			{
				cmd.Parameters.Add(new SqlParameter("@Id", tarea.Id));
				cmd.Parameters.Add(new SqlParameter("@Titulo", tarea.Titulo));
				cmd.Parameters.Add(new SqlParameter("@Descripcion", (object?)tarea.Descripcion ?? DBNull.Value));
				cmd.Parameters.Add(new SqlParameter("@FechaVencimiento", (object?)tarea.FechaVencimiento ?? DBNull.Value));
				cmd.Parameters.Add(new SqlParameter("@UsuarioAsignadoId", (object?)tarea.UsuarioAsignadoId ?? DBNull.Value));
				cmd.Parameters.Add(new SqlParameter("@EstadoId", tarea.EstadoId));
			});

        public Task EliminarAsync(int id) =>
			_bd.EjecutarAsync("dbo.spTareas_Eliminar",
			 cmd => cmd.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@Id", id)));

        public Task<(int EstadoId, string EstadoNombre)> CambiarEstadoAsync(int id, int nuevoEstadoId) =>
			_bd.LeerUnoAsync("dbo.spTareas_CambiarEstado",
				r => ((int)r["EstadoId"], (string)r["EstadoNombre"]),
				cmd =>
				{
					cmd.Parameters.Add(new SqlParameter("@Id", id));
					cmd.Parameters.Add(new SqlParameter("@EstadoId", nuevoEstadoId));
				})!;
	}
}
