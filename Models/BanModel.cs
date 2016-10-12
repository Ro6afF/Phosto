using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Phosto.Models
{
    public class BanModel
    {
        [Key]
        public int id { get; set; }
        public string Username { get; set; }
        public DateTime To { get; set; }
        public string Why { get; set; }
    }
}