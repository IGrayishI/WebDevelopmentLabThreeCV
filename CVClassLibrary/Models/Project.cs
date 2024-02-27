using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CVClassLibrary.Models
{
    public class Project
    {

        public Guid Id { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? GithubLink { get; set; }
        
        public Project()
        {
            Id = Guid.NewGuid();
        }
    }
}
