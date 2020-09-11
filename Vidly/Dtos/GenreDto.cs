﻿using System.ComponentModel.DataAnnotations;

namespace Vidly.Dtos
{
    public class GenreDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
    }
}