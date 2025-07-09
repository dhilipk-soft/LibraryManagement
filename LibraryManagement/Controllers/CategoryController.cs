using LibraryManagement.Infrastructure;
using LibraryManagement.Model;
using LibraryManagement.Model.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly LibraryContext dbContext;
        public CategoryController(LibraryContext dbContext) {
            this.dbContext = dbContext;
        }
        
        [HttpGet]
        public IActionResult GetAllCategory()
        {
            return Ok(dbContext.Categoryies.ToList());
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetCategory(Guid id)
        {
            var result = dbContext.Categoryies.FirstOrDefault( s => s.CategoryId == id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddCategory(AddCategoryDto addCategory)
        {
            var category = new Category
            {
                CategoryName = addCategory.CategoryName,
            };

            dbContext.Categoryies.Add(category);
            dbContext.SaveChanges();

            return Ok(category);
        }

    }
}
