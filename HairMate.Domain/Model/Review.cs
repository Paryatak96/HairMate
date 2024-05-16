using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairMate.Domain.Model
{
    public class Review
    {
        public int ReviewId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public string Response { get; set; }
        public int SalonId { get; set; }
        public Salon Salon { get; set; }
        public int UserId { get; set; }
    }
}
