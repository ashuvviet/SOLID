using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementApi.Models
{
    public abstract class Employee
    {
        /// <value>The id.</value>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int BasicPay { get; set; }

        public int HRA { get; set; }

        public int Bonus { get; set; }

        public string InsuranceType { get; set; }

        public bool IsFullTimeEmployee { get; set; }

        public int GetSalary()
        {
            if (IsFullTimeEmployee)
            {
                return BasicPay + HRA + Bonus;
            }
            else
            {
                return BasicPay + HRA;
            }
        }
    }

    public class FullTimeEmployee : Employee
    {
    }

    public class PartTimeEmployee : Employee
    {
    }
}
