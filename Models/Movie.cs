using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovCustMVCAppWithAuthen.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public GenreType GenreType { get; set; }
        public int GenreTypeId { get; set; }
        //public DateTime DateAdded { get; set; }

        [Display(Name = "Release Date")]

        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Number in Stock")]

        [Range(1, 20)]

        public byte NumberInStock { get; set; }

        public byte NumberAvailable { get; set; }
    }
}