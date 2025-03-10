using System.Data;
using System.Data.SqlClient;

namespace OrganLens.Legacy.Data
{
    public static class DbHelper
    {
        public static DataTable ExecuteQuery(string connectionString, string sql)
        {
            try
            {
                using (var conn = new SqlConnection(connectionString))
                using (var cmd = new SqlCommand(sql, conn))
                using (var adapter = new SqlDataAdapter(cmd))
                {
                    var table = new DataTable();
                    adapter.Fill(table);
                    return table;
                }
            }
            catch
            {
                // don't crash the UI
                return null;
            }
        }

        public static int ExecuteNonQuery(string connectionString, string sql)
        {
            try
            {
                using (var conn = new SqlConnection(connectionString))
                using (var cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                return -1;
            }
        }
    }
}
