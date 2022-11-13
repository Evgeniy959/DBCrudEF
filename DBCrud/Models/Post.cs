using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DBCrud.Models
{
    public class Post
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "This is wrong")]
        [MaxLength(255)]
        [MinLength(3, ErrorMessage = "NOT NOT NOT")]
        public string Title { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Content { get; set; }
        public DateTime Date { get; set; }

        //[Required]
        //   [DataType(DataType.Upload] 
        [Display(Name = "Image path : ")]
        public string ImageUrl { get; set; }
    }
}
