using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;

namespace Shop.Controllers
{
    [Route("categories")]
    public class CategoryController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Category>>> Get([FromServices]DataContext context)
        {
            try
            {
                var categories = await context.Categories.AsNoTracking().ToListAsync();
                if(categories == null || categories.Count == 0 )
                    return NotFound(new { message = "Could not get categories."});
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Could not get categories. Error: {ex.Message}"});
            }
            
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Category>> GetById([FromRoute]int id,[FromServices]DataContext context)
        {
            try
            {
                var category = await context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                if(category == null)
                    return NotFound(new { message = "Could not get category."});
                return Ok(category);
            }
            catch (Exception ex)
            {
                
                return BadRequest(new { message = $"Could not get this category. Error: {ex.Message}"});
            }
            
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Category>> Post([FromBody]Category model, [FromServices]DataContext context)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            try
            {
                context.Categories.Add(model);
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Could not create category. Error: {ex.Message}"});
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<Category>> Put([FromRoute]int id, [FromBody] Category model, [FromServices]DataContext context)
        {
            if(id != model.Id)
                return NotFound(new {message = "Category not found"});
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                context.Entry<Category>(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(new { message = $"Could not Update this category (Concurrency exception). Error: {ex.Message}"});
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Could not Update this category. Error: {ex.Message}"});
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<Category>> Delete([FromRoute]int id, [FromServices]DataContext context)
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if(category == null)
                return NotFound(new { message = "Category Not Found"});
            
            try
            {
                context.Categories.Remove(category);
                await context.SaveChangesAsync();
                return Ok(new { message = "Category Removed"});
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Could not Delete this category. Error: {ex.Message}"});
            }
            
        }
    }
}