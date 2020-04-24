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
    [RoutePrefix("api/products")]
    public class ProductsController : ApiController
    {
        private ApplicationDbContext _ctx = new ApplicationDbContext();

        // POST api/products
        [HttpPost]
        [Route("create")]
        public IHttpActionResult CreateProduct(Product productToCreate)
        {
            if (ModelState.IsValid)
            {
                _ctx.Products.Add(productToCreate);
                _ctx.SaveChanges();
                return Ok();
            }
            return BadRequest("Your product model is invalid");
        }

        [HttpGet]
        public IHttpActionResult GetAllProducts()
        {
            return Ok(_ctx.Products.ToList());
        }
        // by putting the {productId} in there, we will automatically have productId mapped to whatever is after the / in the url
        // example: api/products/4 is the same as api/products?productId=4
        [HttpGet]
        [Route("{productId}")]
        public IHttpActionResult GetIndividualProduct(int productId)
        {
            var productToReturn = _ctx.Products.Find(productId);
            if (productToReturn == null)
            {
                return BadRequest("The product you are looking for doesn't exist. Chill");
            }
            return Ok(productToReturn);
        }

        [HttpPut]
        [Route("update")]
        public IHttpActionResult UpdateIndividualProduct([FromUri] int productToUpdateId, [FromBody] Product updatedProduct)
        {
            var currentProduct = _ctx.Products.Find(productToUpdateId);
            if (currentProduct == null)
            {
                return BadRequest("The product you are looking for doesn't exist. Chill and use real ID next time");
            }
            currentProduct.Description = updatedProduct.Description;
            currentProduct.Price = updatedProduct.Price;
            _ctx.SaveChanges();
            return Ok();
        }
        [HttpDelete]
        [Route("delete")]
        public IHttpActionResult DeleteInidividualProduct([FromUri] int productToDeleteId)
        {
            var productToDelete = _ctx.Products.Find(productToDeleteId);
            if (productToDelete == null)
            {
                return BadRequest("The product you are looking for doesn't exist. Chill and use real ID next time");
            }
            _ctx.Products.Remove(productToDelete);
            return Ok();
        }
    }
}
