using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Vidly.Models
{
    [Table("Genres")]
    public class Genre
    {
        public byte Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
    }
}
