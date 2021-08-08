using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;

namespace WsBenchmark.Controllers.ControlFlowIntra
{
    public class IfShortController : Controller
    {
        private const string SConnect = @"SERVER = .; DATABASE = MYDB; INTEGRATED SECURITY = TRUE";
        private readonly Random _rand = new Random();
        // GET
        public string Index()
        {
            return "IfShortController";
        }
        
        [HttpGet]
        [Route("ifShort/bad/{id}")]
        public string Bad(string id)
        {
            var searchId = _rand.NextDouble() < 0.5 ? "guest" : id;
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
        [Route("ifShort/good/{id}")]
        public string Good(string id)
        {
            var searchId = _rand.NextDouble() < 0.5 ? "guest" : id;
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