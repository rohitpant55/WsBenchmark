using System;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;

namespace WsBenchmark.Controllers.ControlFlowIntra
{
    public class IfController : Controller
    {
        private const string SConnect = @"SERVER = .; DATABASE = MYDB; INTEGRATED SECURITY = TRUE";
        // GET
        public string Index()
        {
            return "IfController";
        }
        
        [HttpGet]
        [Route("if/bad/{id}")]
        public string Bad(string id)
        {
            bool b = false;
            string searchId;
            if (b)
            {
                searchId = "guest";
            }
            else
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
        [Route("if/good/{id}")]
        public string Good(string id)
        {
            bool b = true;
            string searchId;
            if (b)
            {
                searchId = "guest";
            }
            else
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