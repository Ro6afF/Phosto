using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Phosto.Models
{
    public class ImageModel
    {
        [Key]
        public int id { get; set; }
        public string Author { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
