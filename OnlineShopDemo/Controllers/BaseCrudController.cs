using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using OnlineShopDemo;

namespace OnlineShopDemo.Controllers
{
    public interface IBaseCrudController<T> where T : class
    {
        public IActionResult Delete(int key);

        public IActionResult Get();

        public IActionResult Get(int key);

        public IActionResult Post([FromBody] T model);

        public IActionResult Patch(int key, [FromBody] Delta<T> model);
    }

    public class BaseCrudController<T> : ODataController, IBaseCrudController<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;

        public BaseCrudController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpDelete]
        [EnableQuery(MaxExpansionDepth = 3)]
        public IActionResult Delete(int key)
        {
            var record = _dbContext.Set<T>().Find(key);

            if (record == null)
            {
                return NotFound($"Not found {nameof(record)} with id = {key}");
            }

            var dbSet = _dbContext.Set<T>();
            dbSet.Remove(record);
            _dbContext.SaveChanges();

            return Ok();
        }

        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 3)]
        public IActionResult Get()
        {
            return Ok(_dbContext.Set<T>());
        }

        
        [HttpGet("{key}")]
        [EnableQuery(MaxExpansionDepth = 3)]
        public IActionResult Get(int key)
        {
            var record = _dbContext.Set<T>().Find(key);

            if (record == null)
            {
                return NotFound($"Not found {nameof(record)} with id = {key}");
            }

            return Ok(record);
        }

        [HttpPost]
        [EnableQuery(MaxExpansionDepth = 3)]
        public IActionResult Post([FromBody] T model)
        {
            var dbSet = _dbContext.Set<T>();

            dbSet.Add(model);
            _dbContext.SaveChanges();

            return Created(model);
        }

        [HttpPatch]
        [EnableQuery(MaxExpansionDepth = 3)]
        public IActionResult Patch(int key, [FromBody] Delta<T> model)
        {
            var record = _dbContext.Set<T>().Find(key);

            if (record == null)
            {
                return NotFound($"Not found {nameof(model)} with id = {key}");
            }

            model.Patch(record);
            _dbContext.SaveChanges();

            return Updated(record);
        }
    }
}
