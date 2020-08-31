using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vidly.Domain
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        public DateTime ReleaseDate { get; set; }
        public DateTime DateAdded { get; set; }
        public int NumberInStock { get; set; }

        [Required]
        public Genre Genre { get; set; }
        public byte GenreId { get; set; }
    }
}
