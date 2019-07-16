using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ExploreCalifornia.Models
{
    public class Post
    {
        public long Id { get; set; }

        private string _key;
        public string Key
        {
            get
            {
                if (_key == null)
                {
                    var title = !String.IsNullOrWhiteSpace(Title) ? Title : "blog";
                    _key = Regex.Replace(title.ToLower(), "[^a-z0-9]", "-");
                }
                return _key;
            }
            set
            {
                _key = value;
            }
        }

        [Display(Name = "Post tile")]
        [DataType(DataType.Text)]
        [Required]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Title must be between 5 and 100 characters long")]
        public string Title { get; set; }
        public DateTime Posted { get; set; }
        public string Author { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = "Blog posts must be at least 100 characters long")]
        public string Body { get; set; }
    }
}
