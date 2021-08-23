using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;

namespace Shop.Controllers
{
    [Route("products")]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Product>>> Get([FromServices] DataContext context)
        {
            try
            {
                var products = await context.Products.Include(x => x.Category).AsNoTracking().ToListAsync();
                if(products == null || products.Count == 0 )
                    return NotFound(new { message = "products not found."});
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Could not get products. Error: {ex.Message}"});
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Product>> GetById([FromRoute] int id, [FromServices] DataContext context)
        {
            try
            {
                var product = await context.Products.Include(x => x.Category).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                if(product == null)
                    return NotFound(new { message = "product not found."});
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Could not get product. Error: {ex.Message}"});
            }   
        }
        [HttpGet]
        [Route("categories/{id:int}")]
        public async Task<ActionResult<List<Product>>> GetByCategory([FromRoute] int id,[FromServices] DataContext context)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
                if(category == null)
                    return NotFound(new { message = "Category not found..."});
                var products = await context.Products.Include(x => x.Category).AsNoTracking().Where(x => x.CategoryId == id).ToListAsync();
                if(products == null || products.Count == 0 )
                    return NotFound(new { message = "products not found."});
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Could not get products. Error: {ex.Message}"});
            } 
        }
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Product>> Post([FromBody]Product model, [FromServices]DataContext context)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            try
            {
                context.Products.Add(model);
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Could not create Product. Error: {ex.Message}"});
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<Product>> Put([FromRoute]int id, [FromBody] Product model, [FromServices]DataContext context)
        {
            if(id != model.Id)
                return NotFound(new {message = "Product not found"});
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                context.Entry<Product>(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(new { message = $"Could not Update this Product (Concurrency exception). Error: {ex.Message}"});
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Could not Update this Product. Error: {ex.Message}"});
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<Product>> Delete([FromRoute]int id, [FromServices]DataContext context)
        {
            var product = await context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if(product == null)
                return NotFound(new { message = "product Not Found"});
            
            try
            {
                context.Products.Remove(product);
                await context.SaveChangesAsync();
                return Ok(new { message = "product Removed"});
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Could not Delete this product. Error: {ex.Message}"});
            }
            
        }
    }
}