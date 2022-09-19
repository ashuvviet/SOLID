using EmployeeManagementApi.Dto;
using EmployeeManagementApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeDBContext employeeDBContext;

        public EmployeeController(EmployeeDBContext employeeDBContext)
        {
            this.employeeDBContext = employeeDBContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {           
            //return Ok(LiteDBContext.Instance.GetAllEmployees());
            return Ok(employeeDBContext.Employees.Include(x => x.Salary).Include(x => x.Insurance));
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployeeById(int Id)
        {
            return Ok(LiteDBContext.Instance.GetEmployeeById(Id));
        }

        [HttpPost]
        public async Task<IActionResult> InsertEmployee(EmployeeDto employeeDto)
        {
            if(string.IsNullOrEmpty(employeeDto.FirstName))
            {
                throw new InvalidOperationException();
            }

            var e = new Employee() { FirstName = employeeDto.FirstName, LastName = employeeDto.LastName };
            e.Salary = new Payment() { BasicPay = 10, HRA = 12, Bonus = 5 };
            e.Insurance = new Insurance { Name = "X", Amount = 20 };
            var result = employeeDBContext.Employees.Add(e);
            await employeeDBContext.SaveChangesAsync();
            //var result = LiteDBContext.Instance.InsertEmployee();

            // send mail to finance / insurance team


            return Ok(result.State);
        }
    }
}
