using System;
using System.ComponentModel.DataAnnotations;

namespace Phosto.Models
{
    public class CommentModel
    {
        [Key]
        public int id { get; set; }
        public int Commented { get; set; }
        public string Author { get; set; }
        public DateTime Created { get; set; }
        public string Content { get; set; }
    }
}
