using System;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;

namespace WsBenchmark.Controllers.DataFlow
{
    public class MemberAccessController : Controller
    {
        private const string SConnect = @"SERVER = .; DATABASE = MYDB; INTEGRATED SECURITY = TRUE";
        // GET
        public string Index()
        {
            return "MemberAccessController";
        }
        
        [HttpGet]
        [Route("memberAccess/bad/{id}")]
        public string Bad(string id)
        {
            Holder holder = new Holder(id);
            string query = "";
            try
            {
                SqlConnection sqlConnection = new SqlConnection(SConnect);
                query = "SELECT * FROM Users WHERE Id = '" + holder.HolderId + "'";
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
        [Route("memberAccess/good/{id}")]
        public string Good(string id)
        {
            Holder holder = new Holder(id);
            string query = "";
            try
            {
                SqlConnection sqlConnection = new SqlConnection(SConnect);
                query = "SELECT * FROM Users WHERE Id = '" + holder.SafeId + "'";
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
    
    public class Holder
    {
        public string HolderId { get; set; }
        public string SafeId { get; set; }

        public Holder(string holderId)
        {
            HolderId = holderId;
            SafeId = "guest";
        }
    }
}