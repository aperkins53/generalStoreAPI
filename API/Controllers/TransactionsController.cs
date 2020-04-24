using API.Entities;
using API.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI;

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

        [HttpGet]
        public IHttpActionResult ReadAllTransactions([FromBody] Transaction transactionToRead)
        {
            return Ok(_ctx.Transactions.ToList());
        }

        [HttpGet]
        public IHttpActionResult ReadIndividualTransaction(int transactionId)
        {
            var transactionToReturn = _ctx.Transactions.Find(transactionId);
            if (transactionToReturn == null)
            {
                return BadRequest("The transaction doesn't exist.");
            }
            return Ok(transactionToReturn);
        }

        [HttpPut]
        public IHttpActionResult UpdateTransaction([FromUri] int transactionToUpdateId, [FromBody] Transaction updatedTransaction)
        {
            var currentTransaction = _ctx.Transactions.Find(transactionToUpdateId);
            if (currentTransaction == null)
            {
                return BadRequest("The transaction doesn't exist.");
            }
            currentTransaction.TransactionId = updatedTransaction.TransactionId;
            currentTransaction.TransactionTotal = updatedTransaction.TransactionTotal;
            _ctx.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteTransaction([FromUri] int transactionToDeleteId)
        {
            var transactionToDelete = _ctx.Transactions.Find(transactionToDeleteId);
            if (transactionToDelete == null)
            {
                return BadRequest("The transaction doesn't exist.");
            }
            _ctx.Transactions.Remove(transactionToDelete);
            return Ok();
        }
    }
}
