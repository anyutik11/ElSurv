using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace web.Models
{
    public class NewUser
    {
        public string? surname { get; set; }
        public DateTime? dateBirth { get; set; }
        
        public string login { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public string contactEmail { get; set; }
        public string contactPhone { get; set; }
        public string gender { get; set; }

        public string companyId { get; set; }
    }
}

