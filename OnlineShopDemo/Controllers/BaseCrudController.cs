using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OnlineShopDemo.Controllers
{
    public interface IBaseCrudController<T> where T : class
    {
        public IActionResult Delete(int id);

        public IActionResult Get();

        public IActionResult Get(int id);

        public IActionResult Post([FromBody] T model);

        public IActionResult Patch(int id, [FromBody] T model);
    }

    public class BaseCrudController<T> : ControllerBase, IBaseCrudController<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;

        public BaseCrudController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var record = _dbContext.Set<T>().Find(id);

            if (record == null)
            {
                return NotFound($"Not found {nameof(record)} with id = {id}");
            }

            var dbSet = _dbContext.Set<T>();
            dbSet.Remove(record);
            _dbContext.SaveChanges();

            return Ok();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_dbContext.Set<T>());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var record = _dbContext.Set<T>().Find(id);

            if (record == null)
            {
                return NotFound($"Not found {nameof(record)} with id = {id}");
            }

            return Ok(record);
        }

        [HttpPost]
        public IActionResult Post([FromBody] T model)
        {
            var dbSet = _dbContext.Set<T>();

            dbSet.Add(model);
            _dbContext.SaveChanges();

            return Ok(model);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] T model)
        {
            var dbSet = _dbContext.Set<T>();

            var record = dbSet.Find(id);

            if (record == null)
            {
                dbSet.Add(model);
                _dbContext.SaveChanges();

                return Ok(model);
            }

            var modelProp = model.GetType().GetProperties().ToList();
            int i = 0;
            foreach (var item in record.GetType().GetProperties())
            {
                item.SetValue(record, modelProp[i++].GetValue(model));
            }
            
            _dbContext.SaveChanges();

            return Ok(record);
        }
    }
}
