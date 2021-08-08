using System;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;

namespace WsBenchmark.Controllers.ControlFlowInter
{
    public class DelegateController : Controller
    {
        private const string SConnect = @"SERVER = .; DATABASE = MYDB; INTEGRATED SECURITY = TRUE";

        // GET
        public string Index()
        {
            return "DelegateController";
        }

        private delegate string MyDelegate(string msg);

        static string GetQuerySafe(string id)
        {
            return "guest";
        }
        
        static string GetQueryVulnerable(string id)
        {
            return "id_" + id;
        }


        [HttpGet]
        [Route("delegate/bad/{id}")]
        public string Bad(string id)
        {
            MyDelegate myDelegate = new MyDelegate(GetQueryVulnerable);
            string query = "";
            try
            {
                SqlConnection sqlConnection = new SqlConnection(SConnect);
                query = "SELECT * FROM Users WHERE Id = '" + myDelegate(id) + "'";
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
        [Route("delegate/good/{id}")]
        public string Good(string id)
        {
            MyDelegate myDelegate = new MyDelegate(GetQuerySafe);
            string query = "";
            try
            {
                SqlConnection sqlConnection = new SqlConnection(SConnect);
                query = "SELECT * FROM Users WHERE Id = '" + myDelegate(id) + "'";
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