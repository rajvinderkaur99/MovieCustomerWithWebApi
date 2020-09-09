using MovCustMVCAppWithAuthen.Models;
using MovCustMVCAppWithAuthen.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MovCustMVCAppWithAuthen.Controllers
{
    public class MovieController : Controller
    {
        ApplicationDbContext _context=new ApplicationDbContext();
       
        // GET: Movie
        public ActionResult Index()
        {
            IEnumerable<Movie> movies;


            HttpResponseMessage response = GlobalVariables.webApiClient.GetAsync("MovieApi").Result;

            movies = response.Content.ReadAsAsync<IEnumerable<Movie>>().Result;

            if (User.IsInRole("CanManageMovies"))
            {
                
                return View("Index",movies);
            }
            else
            {
               
                return View("ReadOnlyIndex",movies);
            }
           
        }
        public ActionResult Details(int id)
        {
            //-----------------------------------------without WebApi------------------------------------------------------
            //var singleMovie = _context.Movies.Include(m => m.GenreType).SingleOrDefault(m => m.Id == id);
            //var viewmodel = new NewMovieViewModel
            //{
            //    Movie = singleMovie
            //};
            //if (singleMovie == null)
            //    return HttpNotFound();
            //return View(viewmodel);
            //--------------------------------------------------with webapi--------------------------------------------------
            HttpResponseMessage response = GlobalVariables.webApiClient.GetAsync($"MovieApi/{ id}").Result;
            Movie singlemov;
            singlemov = response.Content.ReadAsAsync<Movie>().Result;
            return View(singlemov);
        }
        [HttpGet]
        [Authorize(Roles =RoleName.CanManageMovies)]
        public ActionResult New()
        {
            HttpResponseMessage gresponse = GlobalVariables.webApiClient.GetAsync("GenreApi").Result;
            var genreTypes = gresponse.Content.ReadAsAsync<IEnumerable<GenreType>>().Result;

            //var genretype = _context.GenreTypes.ToList();
            var viewModel = new NewMovieViewModel
            {
                GenreTypes = genreTypes
            };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            //-------------------------------------------without WebApi----------------------------------------------------
            //if (!ModelState.IsValid)
            //{
            //    var ViewModel = new NewMovieViewModel(movie)
            //    {

            //    };
            //    return View("New", ViewModel);
            //}
            //if (movie.Id == 0)
            //    return HttpNotFound();
            //else
            //{
            //    var MovieInDb = _context.Movies.Single(m => m.Id == movie.Id);
            //    MovieInDb.Name = movie.Name;
            //    MovieInDb.GenreTypeId = movie.GenreTypeId;
            //}
            //_context.SaveChanges();
            //return RedirectToAction("Index", "Movie");
            //------------------------------------------------with webapi----------------------------------------------------------
            HttpResponseMessage gresponse = GlobalVariables.webApiClient.GetAsync("GenreApi").Result;
            var genreTypes = gresponse.Content.ReadAsAsync<IEnumerable<GenreType>>().Result;
            HttpResponseMessage mresponse = GlobalVariables.webApiClient.GetAsync("MovieApi").Result;

            if (!ModelState.IsValid)
            {
                var viewModel = new NewMovieViewModel(movie)
                {
                   
                    GenreTypes = genreTypes /*_context.MembershipTypes.ToList()*/
                };

                return View("New", viewModel);
            }

            if (movie.Id == 0)
                //_context.Customers.Add(customer);
                mresponse = GlobalVariables.webApiClient.PostAsJsonAsync("MovieApi", movie).Result;
            else
            {
                mresponse = GlobalVariables.webApiClient.PutAsJsonAsync($"MovieApi/{movie.Id}", movie).Result;
            }
            //if(cresponse.IsSuccessStatusCode)
            //{
            return RedirectToAction("Index");
        }
        [Authorize(Roles=RoleName.CanManageMovies)]
        public ActionResult Edit(int id)
        {
            //--------------------------------------------------without webapi---------------------------------------------
            //var updateMov = _context.Movies.SingleOrDefault(c => c.Id == id);
            //if (updateMov == null)
            //{
            //    return HttpNotFound();
            //}
            //var vm = new NewMovieViewModel
            //{
            //    //Id = updateMov.Id,
            //    //GenreId = updateMov.GenreTypeId,
            //    //Name=updateMov.Nam
            //    Movie = updateMov,
            //    GenreTypes = _context.GenreTypes.ToList()
            //};
            //return View("New", vm);
            //----------------------------------------------------with webapi--------------------------------------------------
            HttpResponseMessage gResponse = GlobalVariables.webApiClient.GetAsync("GenreApi").Result;
            HttpResponseMessage mResponse = GlobalVariables.webApiClient.GetAsync($"MovieApi/{id}").Result;
           var  movie = mResponse.Content.ReadAsAsync<Movie>().Result;
            var vm = new NewMovieViewModel(movie)
            {
                
                GenreTypes = gResponse.Content.ReadAsAsync<IEnumerable<GenreType>>().Result
            };
            return View("New", vm);
        }
        //public ActionResult Create(Movie movie)
        //{
        //    _context.Movies.Add(movie);
        //    _context.SaveChanges();
        //    return RedirectToAction("Index", "Movie");

        //}
        [Authorize(Roles=RoleName.CanManageMovies)]
        public ActionResult Delete(int id)
        {
            var mov = _context.Movies.Find(id);
            _context.Movies.Remove(mov);
            _context.SaveChanges();
            return RedirectToAction("Index", "Movie");

        }
        
    }
}