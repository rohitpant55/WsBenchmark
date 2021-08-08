using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;

namespace WsBenchmark.Controllers.DataFlow
{
    public class ListController : Controller
    {
        private const string SConnect = @"SERVER = .; DATABASE = MYDB; INTEGRATED SECURITY = TRUE";
        // GET
        public string Index()
        {
            return "ListController";
        }
        
        [HttpGet]
        [Route("list/bad/{id}")]
        public string Bad(string id)
        {
            List<string> ids = new List<string> {"user_1", "user_2", "user_3", "user_4"};
            ids[3] = id;
            string query = "";
            try
            {
                SqlConnection sqlConnection = new SqlConnection(SConnect);
                query = "SELECT * FROM Users WHERE Id = '" + ids[3] + "'";
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
        [Route("list/good/{id}")]
        public string Good(string id)
        {
            List<string> ids = new List<string> {"user_1", "user_2", "user_3", "user_4"};
            ids[3] = id;
            string query = "";
            try
            {
                SqlConnection sqlConnection = new SqlConnection(SConnect);
                query = "SELECT * FROM Users WHERE Id = '" + ids[0] + "'";
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