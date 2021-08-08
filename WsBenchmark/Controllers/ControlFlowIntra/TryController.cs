using System;
using System.Data.SqlClient;
using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace WsBenchmark.Controllers.ControlFlowIntra
{
    public class TryController : Controller
    {
        private const string SConnect = @"SERVER = .; DATABASE = MYDB; INTEGRATED SECURITY = TRUE";
        // GET
        public string Index()
        {
            return "TryController";
        }
        
        [HttpGet]
        [Route("try/bad/{id}")]
        public string Bad(string id)
        {
            string searchId = "guest";
            try
            {
                var path = Path.Combine(Path.GetTempPath(), "myTempFile.txt");
                System.IO.File.Delete(path);
                searchId = "guest";
            }
            catch (Exception exception)
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
        [Route("try/good/{id}")]
        public string Good(string id)
        {
            string searchId = "guest";
            try
            {
                searchId = id;
                throw new Exception();
            }
            catch (Exception exception)
            {
                searchId = "guest";
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