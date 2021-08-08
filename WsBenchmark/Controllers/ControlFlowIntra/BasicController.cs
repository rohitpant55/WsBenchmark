using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;

namespace WsBenchmark.Controllers.ControlFlowIntra
{
    public class BasicController : Controller
    {
        private const string SConnect = @"SERVER = .; DATABASE = MYDB; INTEGRATED SECURITY = TRUE";

        // GET
        public string Index()
        {
            return "BasicController";
        }
        
        [HttpGet]
        [Route("basic/bad/{id}")]
        public string Bad(string id)
        {
            id = "id_" + id;
            string query = "SELECT * FROM Users WHERE Id = '" + id + "'";
            try
            {
                SqlConnection sqlConnection = new SqlConnection(SConnect);
                // sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.ExecuteNonQuery();
                // sqlConnection.Close();
            }
            catch (Exception ignore)
            {
            }
            return query;
        }

        [HttpGet]
        [Route("basic/good/{id}")]
        public string Good(string id)
        {
            id = "id_" + id;
            string query = "SELECT * FROM Users WHERE Id = @id";
            try
            {
                SqlConnection sqlConnection = new SqlConnection(SConnect);
                // sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.Add("@id", SqlDbType.Text);
                sqlCommand.Parameters["@id"].Value = id;
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