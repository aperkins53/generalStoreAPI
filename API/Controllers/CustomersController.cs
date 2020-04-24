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
    [RoutePrefix("api/customers")]
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _ctx = new ApplicationDbContext();

        [HttpPost]
        [Route("create")]
        public IHttpActionResult CreateCustomer([FromBody] Customer customerToCreate)
        {
            _ctx.Customers.Add(customerToCreate);
            _ctx.SaveChanges();
            return Ok();
        }
    }
}
