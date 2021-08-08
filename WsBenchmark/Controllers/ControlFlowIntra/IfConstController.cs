using System;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;

namespace WsBenchmark.Controllers.ControlFlowIntra
{
    public class IfConstController : Controller
    {
        private const string SConnect = @"SERVER = .; DATABASE = MYDB; INTEGRATED SECURITY = TRUE";
        // GET
        public string Index()
        {
            return "IfConstController";
        }
        
        [HttpGet]
        [Route("ifConst/bad/{id}")]
        public string Bad(string id)
        {
            string searchId = "guest";
            if (true)
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
        [Route("ifConst/good/{id}")]
        public string Good(string id)
        {
            string searchId = "guest";
            if (false)
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