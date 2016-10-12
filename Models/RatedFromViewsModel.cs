using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Phosto.Models
{
    public class RatedFromViewsModel
    {
        [Key]
        public int id { get; set; }
        public int? iId { get; set; }
        public string user { get; set; }
    }
}