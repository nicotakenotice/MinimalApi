using System.Data.SqlClient;
using System.Text;

namespace MinimalApi.Services {
	public class ApiService {
		public static readonly string connString = "Data Source=localhost;Initial Catalog=test;Integrated Security=True";

		/* ===================================================================== */

		public static async Task<string> TestConnection() {
			StringBuilder sb = new();
			sb.AppendLine("[MinimalApi]");
			sb.AppendLine();
			sb.AppendLine($"Connection to \"{connString}\"...");
			try {
				using SqlConnection conn = new(connString);
				await conn.OpenAsync();
				sb.AppendLine("Connection estabilished!");
				sb.AppendLine("Have fun!");
			}
			catch (Exception ex) {
				sb.AppendLine("Connection failed!");
				sb.AppendLine(ex.Message);
			}
			return sb.ToString();
		}

		public static async Task<List<object>> GetItems() {
			List<object> items = new();
			string cmdString = $"select * from dbo.test";
			try {
				using SqlConnection conn = new(connString);
				await conn.OpenAsync();
				SqlCommand cmd = new(cmdString, conn);
				using SqlDataReader reader = await cmd.ExecuteReaderAsync();
				while (reader.Read()) {
					items.Add(new {
						id = reader.GetValue(0),
						name = reader.GetValue(1)
					});
				}
			}
			catch (Exception ex) {
				System.Diagnostics.Debug.WriteLine(ex.Message);
			}
			return items;
		}

		public static async Task<object?> GetItem(int id) {
			object? obj = null;
			string cmdString = $"select * from dbo.test where id = ${id}";
			try {
				using SqlConnection conn = new(connString);
				await conn.OpenAsync();
				SqlCommand cmd = new(cmdString, conn);
				using SqlDataReader reader = await cmd.ExecuteReaderAsync();
				while (reader.Read()) {
					obj = new {
						id = reader.GetValue(0),
						name = reader.GetValue(1)
					};
				}
			}
			catch (Exception ex) {
				System.Diagnostics.Debug.WriteLine(ex.Message);
			}
			return obj;
		}
	}
}
