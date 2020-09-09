using MovCustMVCAppWithAuthen.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovCustMVCAppWithAuthen.ViewModel
{
    public class NewMovieViewModel
    {
        public IEnumerable<GenreType> GenreTypes { get; set; }
        //public Movie Movie { get; set; }
        public int? Id { get; set; }
        public int? GenreTypeId { get; set; }
        [Required]
        public string Name { get; set; }
        [Display(Name = "Release Date")]

        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Number in Stock")]

        [Range(1, 20)]

        public byte NumberInStock { get; set; }

        public byte NumberAvailable { get; set; }
        public NewMovieViewModel()
        {
            Id = 0;
        }
        public NewMovieViewModel(Movie movie)
        {
            Id = movie.Id;
            Name = movie.Name;
            GenreTypeId = movie.GenreTypeId;
            ReleaseDate = movie.ReleaseDate;
            NumberInStock = movie.NumberInStock;
            NumberAvailable = movie.NumberAvailable;

        }

    }
}