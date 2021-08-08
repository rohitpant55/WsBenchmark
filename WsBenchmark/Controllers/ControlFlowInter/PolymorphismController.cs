using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;

namespace WsBenchmark.Controllers.ControlFlowInter
{
    public class PolymorphismController : Controller
    {
        private const string SConnect = @"SERVER = .; DATABASE = MYDB; INTEGRATED SECURITY = TRUE";
        // GET
        public string Index()
        {
            return "PolymorphismController";
        }
        
        [HttpGet]
        [Route("polymorphism/bad/{id}")]
        public string Bad(string id)
        {
            Base myBase = new ChildUnsafe();
            string query = "";
            try
            {
                SqlConnection sqlConnection = new SqlConnection(SConnect);
                // sqlConnection.Open();
                SqlCommand sqlCommand = myBase.GetCommand(id, sqlConnection);
                query = sqlCommand.CommandText;
                sqlCommand.ExecuteNonQuery();
                // sqlConnection.Close();
            }
            catch (Exception ignore)
            {
            }
            return query;
        }
        
        [HttpGet]
        [Route("polymorphism/good/{id}")]
        public string Good(string id)
        {
            Base myBase = new ChildSafe();
            string query = "";
            try
            {
                SqlConnection sqlConnection = new SqlConnection(SConnect);
                // sqlConnection.Open();
                SqlCommand sqlCommand = myBase.GetCommand(id, sqlConnection);
                query = sqlCommand.CommandText;
                sqlCommand.ExecuteNonQuery();
                // sqlConnection.Close();
            }
            catch (Exception ignore)
            {
            }
            return query;
        }
        
    }
    
    public abstract class Base
    {
        public abstract SqlCommand GetCommand(string id, SqlConnection connection);
    }

    public class ChildSafe : Base
    {
        public ChildSafe()
        {
        }

        public override SqlCommand GetCommand(string id, SqlConnection connection)
        {
            string query = "SELECT * FROM Users WHERE Id = @id";
            SqlCommand sqlCommand = new SqlCommand(query, connection);
            sqlCommand.Parameters.Add("@id", SqlDbType.Text);
            sqlCommand.Parameters["@id"].Value = id;
            return sqlCommand;
        }
    }
    
    public class ChildUnsafe : Base
    {
        public ChildUnsafe()
        {
        }

        public override SqlCommand GetCommand(string id, SqlConnection connection)
        {
            string query = "SELECT * FROM Users WHERE Id = '" + id + "'";
            SqlCommand sqlCommand = new SqlCommand(query, connection);
            return sqlCommand;
        }
    }
}