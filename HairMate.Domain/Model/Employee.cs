using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairMate.Domain.Model
{
    public class Employee
    { 
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        public int SalonId { get; set; }
        public Salon Salon { get; set; }
    }
}
