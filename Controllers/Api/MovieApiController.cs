using MovCustMVCAppWithAuthen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;

namespace MovCustMVCAppWithAuthen.Controllers.Api
{
    public class MovieApiController : ApiController
    {
        private ApplicationDbContext _context;
        public MovieApiController()
        {
            _context = new ApplicationDbContext();
        }
        //GET /api/movieApi
        public IHttpActionResult GetMovies()
        {
            var movies = _context.Movies.Include(m=>m.GenreType).ToList();
            //return _context.Customers.ToList();
            if (movies == null)
            {
                return NotFound();
            }
            return Ok(movies);
        }



        public IHttpActionResult GetMovie(int id)
        {
            if (id <= 0)
                return BadRequest("Not a Valid Movie id");
            var movies = _context.Movies.Include(m=>m.GenreType).SingleOrDefault(c => c.Id == id);
            if (movies == null)
                //  throw new HttpResponseException(HttpStatusCode.NotFound);
                return NotFound();
            return Ok(movies);
        }
        //POST /api/MovieApi
        [HttpPost]
        public IHttpActionResult CreateMovie(Movie movie)
        {
            if (!ModelState.IsValid)
                //throw new HttpResponseException(HttpStatusCode.BadRequest);
                return BadRequest("Model data is invalid");
            _context.Movies.Add(movie);
            _context.SaveChanges();
            return Ok(movie);
            
        }



        //PUT /api/MovieApi/1
        [HttpPut]
        public IHttpActionResult UpdateMovie(int id, Movie movie)
        {
            if (!ModelState.IsValid)
                //throw new HttpResponseException(HttpStatusCode.BadRequest);
                return BadRequest("Invalid data");
            var movieInDb = _context.Movies.SingleOrDefault(c => c.Id == id);
            if (movieInDb == null)
                // throw new HttpResponseException(HttpStatusCode.NotFound);
                return NotFound();
            movieInDb.Name = movie.Name;
            movieInDb.ReleaseDate = movie.ReleaseDate;
            movieInDb.NumberInStock = movie.NumberInStock;
            movieInDb.NumberAvailable = movie.NumberAvailable;
            movieInDb.GenreTypeId = movie.GenreTypeId;
            _context.SaveChanges();
            return Ok();


        }
        //DELETE /api/MovieApi/1
        public IHttpActionResult DeleteMovie(int id)
        {
            if (id <= 0)
                return BadRequest("Not a Valid Movie id");
            var movieInDb = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (movieInDb == null)
                // throw new HttpResponseException(HttpStatusCode.NotFound);
                return NotFound();
            _context.Movies.Remove(movieInDb);
            _context.SaveChanges();
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
