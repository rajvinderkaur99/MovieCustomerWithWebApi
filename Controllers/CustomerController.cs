using Microsoft.Owin.Security.Provider;
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
    [Authorize]
    public class CustomerController : Controller
    {
        private ApplicationDbContext _context;
        // GET: Customer
        public CustomerController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult Index()

        {

            IEnumerable<Customer> customers;


            HttpResponseMessage response = GlobalVariables.webApiClient.GetAsync("Customers").Result;

            customers = response.Content.ReadAsAsync<IEnumerable<Customer>>().Result;

            return View(customers);

        }
        public ActionResult Details(int id)
        {
            //displaying single cust without api
            //var singleCustomer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c=>c.Id==id);
            //if (singleCustomer == null)
            //    return HttpNotFound();
            //return View(singleCustomer);
            //Below code is displaying single cust with webapi
            HttpResponseMessage response = GlobalVariables.webApiClient.GetAsync($"Customers/{ id}").Result;
            Customer singleCust;
            singleCust = response.Content.ReadAsAsync<Customer>().Result;
            return View(singleCust);


        }
        public ActionResult New()
        {
            HttpResponseMessage mresponse = GlobalVariables.webApiClient.GetAsync("MemberShipTypes").Result;
            var memberhipTypes = mresponse.Content.ReadAsAsync<IEnumerable<MembershipType>>().Result;

            //var membershiptype = _context.MembershipTypes.ToList();
            var viewModel = new NewCustomerViewModel
            {
                MembershipTypes = memberhipTypes
            };
            return View(viewModel);
        }
        //[HttpPost]
        //public ActionResult Create(Customer customer)
        //{
        //    _context.Customers.Add(customer);
        //    _context.SaveChanges();
        //    return RedirectToAction("Index", "Customer");
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            HttpResponseMessage mresponse = GlobalVariables.webApiClient.GetAsync("MemberShipTypes").Result;
            var memberhipTypes = mresponse.Content.ReadAsAsync<IEnumerable<MembershipType>>().Result;
            HttpResponseMessage cresponse = GlobalVariables.webApiClient.GetAsync("Customers").Result;

            if (!ModelState.IsValid)
            {
                var viewModel = new NewCustomerViewModel
                {
                   
                    MembershipTypes = memberhipTypes /*_context.MembershipTypes.ToList()*/
                    


                };

                return View("New", viewModel);
            }

            if (customer.Id == 0)
                //_context.Customers.Add(customer);
                cresponse = GlobalVariables.webApiClient.PostAsJsonAsync("Customers", customer).Result;
            else
            {
                cresponse = GlobalVariables.webApiClient.PutAsJsonAsync($"Customers/{customer.Id}", customer).Result;
            }
            //if(cresponse.IsSuccessStatusCode)
            //{
                return RedirectToAction("Index");
            //}
            //else
            //{
            //    var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);
            //    customerInDb.Name = customer.Name;
            //    customerInDb.BirthDate = customer.BirthDate;
            //    customerInDb.MembershipTypeId = customer.MembershipTypeId;
            //    customerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;

            //}

            //_context.SaveChanges();
            //return RedirectToAction("Index", "Customer");
        }
        public ActionResult Edit(int id,Customer customer)
        {
            HttpResponseMessage mResponse = GlobalVariables.webApiClient.GetAsync("MemberShipTypes").Result;
            HttpResponseMessage cResponse = GlobalVariables.webApiClient.GetAsync($"Customers/{id}").Result;
            customer = cResponse.Content.ReadAsAsync<Customer>().Result;
            var vm = new NewCustomerViewModel
            {
                
                MembershipTypes = mResponse.Content.ReadAsAsync<IEnumerable<MembershipType>>().Result
            };
            return View("New", vm);

                

            

            
            //var updateCust = _context.Customers.SingleOrDefault(c => c.Id == id);
            //if (updateCust == null)
            //{
            //    return HttpNotFound();
            //}
            //var vm = new NewCustomerViewModel
            //{
            //    Customer = updateCust,
            //    MembershipTypes = _context.MembershipTypes.ToList()
            //};

            //return View("New",vm);
      
        }
        public ActionResult Delete(int id)
        {
            var cust = _context.Customers.Find(id);
            _context.Customers.Remove(cust);
            _context.SaveChanges();
            return RedirectToAction("Index", "Customer");

        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}