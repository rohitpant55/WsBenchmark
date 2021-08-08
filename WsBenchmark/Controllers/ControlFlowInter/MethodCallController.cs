using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;

namespace WsBenchmark.Controllers.ControlFlowInter
{
    public class MethodCallController : Controller
    {
        private const string SConnect = @"SERVER = .; DATABASE = MYDB; INTEGRATED SECURITY = TRUE";
        // GET
        public string Index()
        {
            return "MethodCallController";
        }
        
        private SqlCommand GetCommandSafe(string id, SqlConnection connection)
        {
            string query = "SELECT * FROM Users WHERE Id = @id";
            SqlCommand sqlCommand = new SqlCommand(query, connection);
            sqlCommand.Parameters.Add("@id", SqlDbType.Text);
            sqlCommand.Parameters["@id"].Value = id;
            return sqlCommand;
        }
        
        private SqlCommand GetCommandUnsafe(string id, SqlConnection connection)
        {
            string query = "SELECT * FROM Users WHERE Id = '" + id + "'";
            SqlCommand sqlCommand = new SqlCommand(query, connection);
            return sqlCommand;
        }
        
        [HttpGet]
        [Route("methodCall/bad/{id}")]
        public string Bad(string id)
        {
            string query = "";
            try
            {
                SqlConnection sqlConnection = new SqlConnection(SConnect);
                // sqlConnection.Open();
                SqlCommand sqlCommand = GetCommandUnsafe(id, sqlConnection);
                query = sqlCommand.CommandText;
                sqlCommand.ExecuteNonQuery();
                // sqlConnection.Close();
            }
            catch (Exception ignore)
            {
            }
            return query;
        }
        
        [HttpGet]
        [Route("methodCall/good/{id}")]
        public string Good(string id)
        {
            string query = "";
            try
            {
                SqlConnection sqlConnection = new SqlConnection(SConnect);
                // sqlConnection.Open();
                SqlCommand sqlCommand = GetCommandSafe(id, sqlConnection);
                query = sqlCommand.CommandText;
                sqlCommand.ExecuteNonQuery();
                // sqlConnection.Close();
            }
            catch (Exception ignore)
            {
            }
            return query;
        }
    }
}