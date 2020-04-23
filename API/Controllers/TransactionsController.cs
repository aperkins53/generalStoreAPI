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
    public class TransactionsController : ApiController
    {
        private ApplicationDbContext _ctx = new ApplicationDbContext();

        [HttpPost]
        public IHttpActionResult CreateTransaction([FromBody] Transaction transactionToCreate)
        {
            try
            {
            _ctx.Transactions.Add(transactionToCreate);
            _ctx.SaveChanges();
            return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
    }
}
