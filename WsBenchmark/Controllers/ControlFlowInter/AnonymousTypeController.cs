using System;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;

namespace WsBenchmark.Controllers.ControlFlowInter
{
    public class AnonymousTypeController : Controller
    {
        private const string SConnect = @"SERVER = .; DATABASE = MYDB; INTEGRATED SECURITY = TRUE";
        // GET
        public string Index()
        {
            return "AnonymousTypeController";
        }
        
        [HttpGet]
        [Route("anonymousType/bad/{id}")]
        public string Bad(string id)
        {
            var ids = new {a = "id_1", b = "id_2", c = "id_3", d = id};
            string query = "";
            try
            {
                SqlConnection sqlConnection = new SqlConnection(SConnect);
                query = "SELECT * FROM Users WHERE Id = '" + ids.d + "'";
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
        [Route("anonymousType/good/{id}")]
        public string Good(string id)
        {
            var ids = new {a = "id_1", b = "id_2", c = "id_3", d = id};
            string query = "";
            try
            {
                SqlConnection sqlConnection = new SqlConnection(SConnect);
                query = "SELECT * FROM Users WHERE Id = '" + ids.a + "'";
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
    }
}