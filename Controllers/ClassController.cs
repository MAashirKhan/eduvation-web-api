using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Eduvation.Models;

namespace Eduvation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly IConfiguration _configuration;


        public ClassController(IConfiguration configuration) 
        {
            _configuration = configuration;

        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"Select ClassID, ClassName From dbo.tbl_Class";

            DataTable dt = new DataTable();
            string SqlDataSource = _configuration.GetConnectionString("StudentAppCon");

            SqlDataReader reader;

            using (SqlConnection con = new SqlConnection(SqlDataSource))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    reader = cmd.ExecuteReader();
                    dt.Load(reader);

                    reader.Close();
                    con.Close();
                }
            }

            return new JsonResult(dt);
           
        }

        [HttpPost]
        public JsonResult Post(Class stdclass)
        {
            string query = @"Insert into dbo.tbl_Class values('"+stdclass.ClassName+@"')";

            DataTable dt = new DataTable();
            string SqlDataSource = _configuration.GetConnectionString("StudentAppCon");

            SqlDataReader reader;

            using (SqlConnection con = new SqlConnection(SqlDataSource))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    reader = cmd.ExecuteReader();
                    dt.Load(reader);

                    reader.Close();
                    con.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Class stdclass)
        {
            string query = @"Update dbo.tbl_Class set ClassName = '" + stdclass.ClassName + @"'
                           where ClassID = '"+stdclass.ClassID+@"'
                           ";

            DataTable dt = new DataTable();
            string SqlDataSource = _configuration.GetConnectionString("StudentAppCon");

            SqlDataReader reader;

            using (SqlConnection con = new SqlConnection(SqlDataSource))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    reader = cmd.ExecuteReader();
                    dt.Load(reader);

                    reader.Close();
                    con.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{ID}")]
        public JsonResult Delete(int ID)
        {
            string query = @"Delete from dbo.tbl_Class
                           where ClassID = '" + ID + @"'
                           ";

            DataTable dt = new DataTable();
            string SqlDataSource = _configuration.GetConnectionString("StudentAppCon");

            SqlDataReader reader;

            using (SqlConnection con = new SqlConnection(SqlDataSource))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    reader = cmd.ExecuteReader();
                    dt.Load(reader);

                    reader.Close();
                    con.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }
    }
}
