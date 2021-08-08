using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;

namespace WsBenchmark.Controllers.DataFlow
{
    public class StaticFieldController : Controller
    {
        private const string SConnect = @"SERVER = .; DATABASE = MYDB; INTEGRATED SECURITY = TRUE";
        private static string StaticFieldId { get; set; }
        
        // GET
        public string Index()
        {
            return "StaticFieldController";
        }
        
        [HttpGet]
        [Route("staticField/setStatic/{id}")]
        public void SetStaticId(string id)
        {
            StaticFieldId = id;
        }
        
        [HttpGet]
        [Route("staticField/bad")]
        public string Bad()
        {
            string query = "SELECT * FROM Users WHERE Id = '" + StaticFieldId + "'";
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
        [Route("staticField/good")]
        public string Good()
        {
            string query = "SELECT * FROM Users WHERE Id = @id";
            try
            {
                SqlConnection sqlConnection = new SqlConnection(SConnect);
                // sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.Add("@id", SqlDbType.Text);
                sqlCommand.Parameters["@id"].Value = StaticFieldId;
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