using MovCustMVCAppWithAuthen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MovCustMVCAppWithAuthen.Controllers.Api
{
    public class GenreApiController : ApiController
    {
        ApplicationDbContext _context;
        public GenreApiController()
        {
            _context = new ApplicationDbContext();
        }
       
        public IHttpActionResult GetAllGenreType()
        {
            IEnumerable<GenreType> genreTypes;
            genreTypes = _context.GenreTypes.ToList();
            if (genreTypes == null)
                return NotFound();
            return Ok(genreTypes);

        }
    }
}
