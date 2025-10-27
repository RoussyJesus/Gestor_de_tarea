using Gestor_de_tareas.Datos;
using Gestor_de_tareas.Modelos;
using Microsoft.Data.SqlClient;

namespace Gestor_de_tareas.Repositorios
{
	public class RepositorioUsuarios
	{
		private readonly ConexionBd _bd;
		public RepositorioUsuarios(ConexionBd bd) => _bd = bd;

		public Task<List<Usuario>> ListarAsync() =>
			_bd.LeerListaAsync("dbo.spUsuarios_Listar", r => new Usuario
			{
				Id = (int)r["Id"],
				Nombres = (string)r["Nombres"],
				Correo = (string)r["Correo"]
			});

		public async Task<int> CrearAsync(string nombres, string correo)
		{
			var nuevoId = await _bd.EscalarAsync("dbo.spUsuarios_Crear", c =>
			{
				c.Parameters.Add(new SqlParameter("@Nombres", nombres));
				c.Parameters.Add(new SqlParameter("@Correo", correo));
			});
			return Convert.ToInt32(nuevoId);
		}

		public Task<List<Estado>> ListarEstadosAsync() =>
			_bd.LeerListaAsync("dbo.spEstadosTarea_Listar", r => new Estado
			{
				Id = (int)r["Id"],
				Nombre = (string)r["Nombre"]
			});
	}
}
