using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Phosto.Models
{
    public class RatingModel
    {
        public RatingModel()
        {
            Views = 0;
            Likes = 0;
            Hates = 0;
        }

        [Key]
        public int id { get; set; }
        public int Views { get; set; }
        public int Likes { get; set; }
        public int Hates { get; set; }
    }
}
