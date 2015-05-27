using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcProjectBlogPage.Models
{
    public class Admin
    {
        [Key]
        public int ID { set; get; }

        [Required]
        public string Name { set; get; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { set; get; }

        [Required]
        public string Password { set; get; }

        public virtual ICollection<Article> ArticleId { set; get; }
    }
}