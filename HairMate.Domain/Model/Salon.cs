using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairMate.Domain.Model
{
    public class Salon
    {
        public int SalonId { get; set; }
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string PaymentType { get; set; }
        public ICollection<Service> Services { get; set; }
        public ICollection<Employee> Employees { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
