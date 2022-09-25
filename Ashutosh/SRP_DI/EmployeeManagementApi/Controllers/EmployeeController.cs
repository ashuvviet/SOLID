using EmployeeManagementApi.Dto;
using EmployeeManagementApi.Models;
using EmployeeManagementApi.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
        public IActionResult GetAll()
        {
            var emps= employeeDBContext.Employees;
            return Ok(emps);
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployeeById(int Id)
        {
            var e = employeeDBContext.Employees.FirstOrDefault(s => s.Id == Id);
            return Ok(e);
        }

        [HttpGet("insurance")]
        public IActionResult GetEmployeeInsurance(int Id)
        {
            var e = employeeDBContext.Employees.FirstOrDefault(s => s.Id == Id);
            if (e is FullTimeEmployee)
            {
                return Ok("Adtiya Birla");
            }
            else if (e is PartTimeEmployee)
            {
                return Ok("Max");
            }

            return NotFound();
        }

        [HttpGet("salary")]
        public IActionResult GetEmployeeSalary(int Id)
        {
            var e = employeeDBContext.Employees.FirstOrDefault(s => s.Id == Id);
            var salary = 0;
            if (e is FullTimeEmployee)
            {
               salary = e.BasicPay + e.HRA + e.Bonus;
            }
            else if (e is PartTimeEmployee)
            {
                salary = e.BasicPay + e.HRA;
            }

            return Ok(salary);
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
                SqlCommand cmd = new SqlCommand("Select * from tablename", connection);

                var employeeQuery = "Insert into Employees (FirstName,LastName,Email,BasicPay,HRA,Bonus,IsFullTimeEmployee, EmpType) VALUES (@1,@2,@3,@4,@5,@6, @7, @8)";

                var result = ExecuteQueryWithNoResult(connection, employeeQuery, employeeDto.FirstName, employeeDto.LastName, employeeDto.Email,1,1,1,false,1);


                return Ok(result);
            }


            //var e = new PartTimeEmployee() { FirstName = employeeDto.FirstName, LastName = employeeDto.LastName, Email = employeeDto.Email  };
            //var result = employeeDBContext.Employees.Add(e);
            //await employeeDBContext.SaveChangesAsync();

            // send mail to finance / insurance team


            //return Ok();
        }

        private bool ExecuteQueryWithNoResult(SqlConnection connection, string query, params object[] paramList)
        {
            // Create the command
            var cmd = new SqlCommand(query, connection);

            // Ensure that query is always in good shape !
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
