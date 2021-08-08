using System;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;

namespace WsBenchmark.Controllers.ControlFlowIntra
{
    public class WhileController : Controller
    {
        private const string SConnect = @"SERVER = .; DATABASE = MYDB; INTEGRATED SECURITY = TRUE";
        private readonly Random _rand = new Random();
        // GET
        public string Index()
        {
            return "WhileController";
        }
        
        [HttpGet]
        [Route("while/bad/{id}")]
        public string Bad(string id)
        {
            string searchId = "guest";
            while (_rand.NextDouble() < 0.5)
            {
                searchId = id;
            }
            string query = "SELECT * FROM Users WHERE Id = '" + searchId + "'";
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
        [Route("while/good/{id}")]
        public string Good(string id)
        {
            string searchId = "guest";
            bool b = false;
            while (b)
            {
                searchId = id;
            }
            string query = "SELECT * FROM Users WHERE Id = '" + searchId + "'";
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
    }
}