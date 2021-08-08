using System;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;

namespace WsBenchmark.Controllers.ControlFlowInter
{
    public class LambdaController : Controller
    {
        private const string SConnect = @"SERVER = .; DATABASE = MYDB; INTEGRATED SECURITY = TRUE";
        // GET
        public string Index()
        {
            return "LambdaController";
        }
        
        [HttpGet]
        [Route("lambda/bad/{id}")]
        public string Bad(string id)
        {
            string Pad(string s) => "_" + s;
            string query = "";
            try
            {
                SqlConnection sqlConnection = new SqlConnection(SConnect);
                query = "SELECT * FROM Users WHERE Id = '" + Pad(id) + "'";
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
        [Route("lambda/good/{id}")]
        public string Good(string id)
        {
            string Clear(string s) => "guest";
            string query = "";
            try
            {
                SqlConnection sqlConnection = new SqlConnection(SConnect);
                query = "SELECT * FROM Users WHERE Id = '" + Clear(id) + "'";
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