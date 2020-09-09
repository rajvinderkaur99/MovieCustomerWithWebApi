using MovCustMVCAppWithAuthen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MovCustMVCAppWithAuthen.Controllers.Api
{
    public class MemberShipTypesController : ApiController
    {
        ApplicationDbContext _context;
        public MemberShipTypesController()
        {
            _context = new ApplicationDbContext();
        }
        public IHttpActionResult GetAllMembrshipTypes()
        {
            IEnumerable<MembershipType> membershipTypes;
            membershipTypes=_context.MembershipTypes.ToList();
            if (membershipTypes == null)
                return NotFound();
            return Ok(membershipTypes);

        }
    }
}
