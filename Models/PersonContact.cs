using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContactApp.Models
{
    public class PersonContact
    {
        public Guid PersonID { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Firma { get; set; }
        public List<string> PersonPhone { get; set; }
        public List<string> PersonMail { get; set; }
        public List<string> PersonLocation { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public string Location { get; set; }

    }
}