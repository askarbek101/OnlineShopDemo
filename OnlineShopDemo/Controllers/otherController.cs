using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using OnlineShopDemo.Models;

namespace OnlineShopDemo.Controllers
{
    [EnableCors("AllowOrigin")]
    [ApiController]
    [Route("api/")]
    public class otherController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public otherController(ApplicationDbContext dbContext) 
        {
            _dbContext = dbContext;
        }


        [HttpGet("{key}")]
        [Route("products/slug")]
        public IActionResult Get(string key)
        {
            var record = _dbContext.Products.Where(q=>q.slug == key).FirstOrDefault();

            if (record == null)
            {
                return NotFound($"Not found {nameof(record)} with id = {key}");
            }

            return Ok(record);
        }
    }
}
