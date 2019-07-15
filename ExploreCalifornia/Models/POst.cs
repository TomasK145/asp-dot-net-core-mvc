using System;
using System.ComponentModel.DataAnnotations;

namespace ExploreCalifornia.Models
{
    public class Post
    {
        [Display(Name = "Post tile")]
        [DataType(DataType.Text)]
        [Required]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Title must be between 5 and 100 characters long")]
        public string Title { get; set; }
        public DateTime Posted { get; set; }
        public string Author { get; set; }
        [Required]
        [MinLength(100, ErrorMessage = "Blog posts must be at least 100 characters long")]
        public string Body { get; set; }
    }
}
