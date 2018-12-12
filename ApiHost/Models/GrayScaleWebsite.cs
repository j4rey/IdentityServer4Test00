using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiHost.Models
{
    public class GrayScaleWebsite
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Title Must be provided")]
        [StringLength(50, MinimumLength = 2)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please provide Name")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Length must be within 2 to 100 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is must")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Must be with 2 to 100 characters")]
        public string Description { get; set; }
        
    }
}
