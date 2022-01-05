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
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Eduvation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;


        public StudentController(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;

        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"Select * From dbo.tbl_Student";

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
        public JsonResult Post(Student stdinfo)
        {
            string query = @"Insert into tbl_Student(StdName, StdProgram, StdEmail) 
                           values('"+stdinfo.StdName+"', '"+stdinfo.StdProgram+"', '"+stdinfo.StdEmail+"')";

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
        public JsonResult Put(Student stdinfo)
        {
            string query = @"Update dbo.tbl_Student set 
                           StdName = '"+stdinfo.StdName+"', StdProgram = '"+stdinfo.StdProgram+"', StdEmail = '"+stdinfo.StdEmail+"' where StdID = '"+stdinfo.StdID+"'";

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
            string query = @"Delete from dbo.tbl_Student where StudID = '" + ID + @"'";

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
