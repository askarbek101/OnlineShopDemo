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
    public class usersController : BaseCrudController<User>
    {
        private readonly ApplicationDbContext _dbContext;

        public usersController(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("login")]
        public IActionResult Post(string email, string password)
        {
            if (_dbContext.Users.Where(q => q.email == email && q.password == password).Count() == 0)
                return NotFound();
            return Ok(); //не совсем правильно но уже голова не варит) 
        }
        [HttpPost("register")]
        public IActionResult Post(string name, string email, string password)
        {
            var record = new User();
            record.name = name;
            record.email = email;
            record.password = password;
            _dbContext.Add(record);
            _dbContext.SaveChanges();

            return Created(record);
        }
        [HttpPatch("update-user")]
        public IActionResult Patch(int key, string name, string email, string password)
        {
            var record = _dbContext.Users.Find(key);
            record.name = name;
            record.email = email;
            record.password = password;
            _dbContext.SaveChanges();

            return Updated(record);
        }
    }
}
