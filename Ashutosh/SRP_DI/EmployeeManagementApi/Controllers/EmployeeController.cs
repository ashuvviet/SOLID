using EmployeeManagementApi.Dto;
using EmployeeManagementApi.Managers;
using EmployeeManagementApi.Models;
using EmployeeManagementApi.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace EmployeeManagementApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeDBContext employeeDBContext;
        private readonly IOptions<DbConfig> dbOptions;

        public EmployeeController(EmployeeDBContext employeeDBContext, IOptions<DbConfig> dbOptions)
        {
            this.employeeDBContext = employeeDBContext;
            this.dbOptions = dbOptions;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = new List<Employee>();
            using (var connection = new SqlConnection(this.dbOptions.Value.PathToDB))
            {
                connection.Open();
                var employeeQuery = "Select * from Employees";
                var cmd = new SqlCommand(employeeQuery, connection);

                var read = cmd.ExecuteReader();
                while (read.Read())
                {
                    var e = new FullTimeEmployee();
                    e.Id = (int)read["id"];
                    e.FirstName = (string)read["FirstName"];
                    e.LastName = (string)read["LastName"];
                    e.Email = (string)read["Email"];
                    result.Add(e);
                }               
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> InsertEmployee(EmployeeDto employeeDto)
        {
            if(string.IsNullOrEmpty(employeeDto.FirstName))
            {
                throw new InvalidOperationException();
            }

            using (var connection = new SqlConnection(this.dbOptions.Value.PathToDB))
            {
                connection.Open();
                var employeeQuery = "Insert into Employees (FirstName,LastName,Email,BasicPay,HRA,Bonus,IsFullTimeEmployee, EmpType) VALUES (@1,@2,@3,@4,@5,@6, @7, @8)";

                var result = ExecuteQueryWithNoResult(connection, employeeQuery, employeeDto.FirstName, employeeDto.LastName, employeeDto.Email,1,1,1,false,1);

                return Ok(result);
            }

            // send mail to finance / insurance team
            var mailService = new SMTPMailService();
            await mailService.SendMail("fiance@danaher.com", "Welcome", "Welcome To Danaher");

            //return Ok();
        }

        private bool ExecuteQueryWithNoResult(SqlConnection connection, string query, params object[] paramList)
        {
            var cmd = new SqlCommand(query, connection);

            var paramCount = 0;
            foreach (var param in paramList)
            {
                var myParam = new SqlParameter(string.Format("@{0}", ++paramCount), SqlDbType.VarChar)
                {
                    Value = param
                };
                cmd.Parameters.Add(myParam);
            }

            // return a Sql data reader that returns number of impacted lines
            return cmd.ExecuteNonQuery() > 0;
        }
    }

   
}
