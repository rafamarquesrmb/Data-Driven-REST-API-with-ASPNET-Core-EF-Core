using Microsoft.AspNetCore.Mvc;
using Shop.Models;

namespace Shop.Controllers
{
    [Route("categories")]
    public class CategoryController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public string Get()
        {
            return "Get";
        }
        [HttpGet]
        [Route("{id:int}")]
        public string Get([FromRoute]int id)
        {
            return $"Get with ID: {id}";
        }

        [HttpPost]
        [Route("")]
        public Category Post([FromBody]Category model)
        {
            return model;
        }

        [HttpPut]
        [Route("{id:int}")]
        public Category Put([FromRoute]int id, [FromBody] Category model)
        {
            if(model.Id == id)
                return model;
            return null;
        }

        [HttpDelete]
        [Route("{id:int}")]
        public string Delete([FromRoute]int id)
        {
            return "Delete";
        }
    }
}