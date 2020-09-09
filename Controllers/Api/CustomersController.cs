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
    //public class CustomersController : ApiController
    //{
    //}
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }
        //GET /api/customers
        public IHttpActionResult GetCustomers()
        {
            var customers = _context.Customers.Include(c => c.MembershipType).ToList();
            //return _context.Customers.ToList();
            if(customers==null)
            {
                return NotFound();
            }
            return Ok(customers);
        }



        public IHttpActionResult GetCustomer(int id)
        {
            if(id<=0)
                return BadRequest("Not a Valid Customer id");
            var customer = _context.Customers.Include(c=>c.MembershipType).SingleOrDefault(c => c.Id == id);
            if (customer == null)
                //  throw new HttpResponseException(HttpStatusCode.NotFound);
                return NotFound();
            return Ok(customer);
        }
        //POST /api/customers
        [HttpPost]
        public IHttpActionResult CreateCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
                //throw new HttpResponseException(HttpStatusCode.BadRequest);
                return BadRequest("Model data is invalid");
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return Ok(customer);
            //_context.Customers.Add(customer);
            //_context.SaveChanges();

            //return CreatedAtRoute("DefaultApi", new { id = customer.Id },Customer customer);
        }



        //PUT /api/customers/1
        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id, Customer customer)
        {
            if (!ModelState.IsValid)
                //throw new HttpResponseException(HttpStatusCode.BadRequest);
                return BadRequest("Invalid data");
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDb == null)
                // throw new HttpResponseException(HttpStatusCode.NotFound);
                return NotFound();
            customerInDb.Name = customer.Name;
            customerInDb.BirthDate = customer.BirthDate;
            customerInDb.MembershipTypeId = customer.MembershipTypeId;
            customerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
            _context.SaveChanges();
            return Ok();


        }
        //DELETE /api/customers/1
        public IHttpActionResult DeleteCustomer(int id)
        {
            if (id <= 0)
                return BadRequest("Not a Valid Customer id");
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                // throw new HttpResponseException(HttpStatusCode.NotFound);
                return NotFound();
            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

    }
}
