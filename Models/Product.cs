using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace IT2166_Assignment.Models
{
    
    public partial class Product
    {
        [Key]
        [StringLength(300)]
 
        public string Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(300)]
        public string Description { get; set; }
        [Required]
        [Column("Date of activity")]
     


        public DateTime DateOfActivity { get; set; }
        [StringLength(500)]

       
        public string ImageUrl { get; set; }
        [Required]
        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile ImageFile { get; set; }


    }
}
