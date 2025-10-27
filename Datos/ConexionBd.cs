using Microsoft.Data.SqlClient;
using System.Data;

namespace Gestor_de_tareas.Datos
{
	public class ConexionBd
	{
		private readonly string _cadenaConexion;

		public ConexionBd(string cadenaConexion)
		{
			_cadenaConexion = cadenaConexion;
		}

		
		private async Task<T> ConectarAsync<T>(Func<SqlConnection, Task<T>> accion)
		{
			await using var conexion = new SqlConnection(_cadenaConexion);
			await conexion.OpenAsync();
			return await accion(conexion);
		}

		
		public async Task<int> EjecutarAsync(string procedimiento, Action<SqlCommand>? parametros = null)
		{
			return await ConectarAsync(async conexion =>
			{
				await using var comando = new SqlCommand(procedimiento, conexion)
				{
					CommandType = CommandType.StoredProcedure
				};
				parametros?.Invoke(comando);
				return await comando.ExecuteNonQueryAsync();
			});
		}

		
		public async Task<object?> EscalarAsync(string procedimiento, Action<SqlCommand>? parametros = null)
		{
			return await ConectarAsync(async conexion =>
			{
				await using var comando = new SqlCommand(procedimiento, conexion)
				{
					CommandType = CommandType.StoredProcedure
				};
				parametros?.Invoke(comando);
				return await comando.ExecuteScalarAsync();
			});
		}

		
		public async Task<List<T>> LeerListaAsync<T>(string procedimiento, Func<IDataReader, T> map, Action<SqlCommand>? parametros = null)
		{
			return await ConectarAsync(async conexion =>
			{
				await using var comando = new SqlCommand(procedimiento, conexion)
				{
					CommandType = CommandType.StoredProcedure
				};
				parametros?.Invoke(comando);

				var lista = new List<T>();
				using var lector = await comando.ExecuteReaderAsync();
				while (await lector.ReadAsync())
				{
					lista.Add(map(lector));
				}
				return lista;
			});
		}

		
		public async Task<T?> LeerUnoAsync<T>(string procedimiento, Func<IDataReader, T> map, Action<SqlCommand>? parametros = null)
		{
			var lista = await LeerListaAsync(procedimiento, map, parametros);
			return lista.FirstOrDefault();
		}
	}
}
