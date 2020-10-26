using EmployeeCRUDWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EmployeeCRUDWebAPI.Controllers
{
    public class DepartmentController : ApiController
    {
        public HttpResponseMessage Get()
        {
            string query = @"
                    select DepartmentId, DepartmentName from 
                    dbo.Department";
            DataTable table = new DataTable();
            using (var conn = new SqlConnection(ConfigurationManager.
                ConnectionStrings["EmployeeCRUDWebAPI"].ConnectionString
                ))
             using (var cmd = new SqlCommand(query, conn))
            using ( var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        public string Post(Department dep)
        {
            try
            {
                string query = @"
                    insert into dbo.department values
                     ('" + dep.DepartmentName + @"') ";

                DataTable table = new DataTable();
                using (var conn = new SqlConnection(ConfigurationManager.
                    ConnectionStrings["EmployeeCRUDWebAPI"].ConnectionString
                    ))
                using (var cmd = new SqlCommand(query, conn))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "Department added sucessfully";
            }
            catch (Exception)
            {

                return "failed to add department";
            }
        }

        public string Put(Department dep)
        {
            try
            {
                string query = @"
                    update dbo.Department set DepartmentName =
                     '" + dep.DepartmentName + @"'  
                      where DepartmentId = "+dep.DepartmentId + @"
                    ";

                DataTable table = new DataTable();
                using (var conn = new SqlConnection(ConfigurationManager.
                    ConnectionStrings["EmployeeCRUDWebAPI"].ConnectionString
                    ))
                using (var cmd = new SqlCommand(query, conn))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "Department updated sucessfully";
            }
            catch (Exception)
            {

                return "Department update failed";
            }
        }
        [HttpDelete]
        public string Delete(int id)
        {
            try
            {
                string query = @"
                   delete from dbo.Department 
                      where DepartmentId = " + id + @"
                    ";

                DataTable table = new DataTable();
                using (var conn = new SqlConnection(ConfigurationManager.
                    ConnectionStrings["EmployeeCRUDWebAPI"].ConnectionString
                    ))
                using (var cmd = new SqlCommand(query, conn))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "Department deleted sucessfully";
            }
            catch (Exception)
            {

                return "failed to delete department";
            }
        }
    }
}
