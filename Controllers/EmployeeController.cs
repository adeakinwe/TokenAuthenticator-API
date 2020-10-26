using EmployeeCRUDWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
namespace EmployeeCRUDWebAPI.Controllers
{
    public class EmployeeController : ApiController
    {
        public HttpResponseMessage Get()
        {
            string query = @"
                    select EmployeeId, EmployeeName, Department,
                    convert(varchar(10),DateOfJoining,120) as DateOfJoining,
                    PhotoFileName                  
                    from dbo.Employee";
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

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        public string Post(Employee emp)
        {
            try
            {
                string query = @"
                    insert into dbo.Employee values
                     ('" + emp.EmployeeName + @"',
                     '" + emp.Department + @"',
                     '" + emp.DateOfJoining + @"',
                     '" + emp.PhotoFileName + @"')";                      
                     
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
                return "Employee added sucessfully";
            }
            catch (Exception)
            {

                return "failed to add employee";
            }
        }

        public string Put(Employee emp)
        {
            try
            {
                string query = @"
                    update dbo.Employee set 
                    EmployeeName = '" + emp.EmployeeName + @"',  
                    Department = '" + emp.Department + @"',  
                    DateOfJoining = '" + emp.DateOfJoining + @"',  
                    PhotoFileName = '" + emp.PhotoFileName+ @"'

                    where EmployeeId = " + emp.EmployeeId + @" ";

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
                return "Employee updated sucessfully";
            }
            catch (Exception)
            {

                return "Employee update failed";
            }
        }

        public string Delete(int id)
        {
            try
            {
                string query = @"
                   delete from dbo.Employee
                      where EmployeeId = " + id + @"
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
                return "Employee deleted sucessfully";
            }
            catch (Exception)
            {

                return "delete employee failed";
            }
        }

        [Route("api/employee/getalldepartment")]
        [HttpGet]
        public HttpResponseMessage GetAllDepartment()
        {
            string query = @"
                    select DepartmentName from dbo.Department";
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

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("api/employee/savefile")]

        public string SaveFile()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                var postedFile = httpRequest.Files[0];
                string fileName = postedFile.FileName;
                var physicalPath = HttpContext.Current.Server.MapPath("~/Photos/" + fileName);

                postedFile.SaveAs(physicalPath);

                return fileName;
            }
            catch (Exception)
            {
                return "anonymous.png";
            }
        }
    }
}
