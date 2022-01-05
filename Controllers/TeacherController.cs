using Eduvation.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Eduvation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : Controller
    {

        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;


        public TeacherController(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;

        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"Select * From dbo.tbl_Teacher";

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
        public JsonResult Post(Teacher teacherinfo)
        {
            string query = @"Insert into tbl_Teacher(TeacherName, TeacherEmail, TeacherSalary) 
                           values('" + teacherinfo.TeacherName + "', '" + teacherinfo.TeacherEmail + "', " + teacherinfo.TeacherSalary+ ")";

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
        public JsonResult Put(Teacher teacherinfo)
        {
            string query = @"Update dbo.tbl_Teacher set 
                           TeacherName = '" + teacherinfo.TeacherName + "', TeacherEmail = '" + teacherinfo.TeacherEmail + "'," +
                           " TeacherSalary = " + teacherinfo.TeacherSalary + " where TeacherID = " + teacherinfo.TeacherID + "";

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
            string query = @"Delete from dbo.tbl_Teacher where TeacherID = '" + ID + @"'";

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
