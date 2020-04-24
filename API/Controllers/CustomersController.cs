using API.Entities;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _ctx = new ApplicationDbContext();

        [HttpPost]
        public IHttpActionResult CreateCustomer([FromBody] Customer customerToCreate)
        {
            _ctx.Customers.Add(customerToCreate);
            _ctx.SaveChanges();
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult ReadAllCustomers([FromBody] Customer customerToRead)
        {
            return Ok(_ctx.Customers.ToList());
        }

        [HttpGet]
        public IHttpActionResult ReadIndividualCustomer(int customerId)
        {
            var customerToReturn = _ctx.Customers.Find(customerId);
            if (customerToReturn == null)
            {
                return BadRequest("The customer doesn't exist.");
            }
            return Ok(customerToReturn);
        }

        [HttpPut]
        public IHttpActionResult UpdateCustomer([FromUri] int customerToUpdateId, [FromBody] Customer updatedCustomer)
        {
            var currentCustomer = _ctx.Customers.Find(customerToUpdateId);
            if (currentCustomer == null)
            {
                return BadRequest("The customer doesn't exist.");
            }
            currentCustomer.CustomerId = updatedCustomer.CustomerId;
            _ctx.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteCustomer([FromUri] int customerToDeleteId)
        {
            var customerToDelete = _ctx.Customers.Find(customerToDeleteId);
            if (customerToDelete == null)
            {
                return BadRequest("The customer doesn't exist.");
            }
            _ctx.Customers.Remove(customerToDelete);
            return Ok();
        }
    }
}
