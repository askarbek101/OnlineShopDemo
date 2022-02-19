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
    [Route("api/[controller]")]
    public class productsController : BaseCrudController<Product>
    {
        private readonly ApplicationDbContext _dbContext;

        public productsController(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("slug")]
        public IActionResult slug(string slug)
        {
            var record = _dbContext.Products.Where(q => q.slug == slug).FirstOrDefault();
            return Ok(record);
        }
        [HttpGet("categories")]
        public IActionResult Get()
        {
            var record = _dbContext.Products.Select(q => q.category).Distinct();
            return Ok(record);
        }
    }
}
