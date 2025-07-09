using LibraryManagement.Application.Interfaces;
using LibraryManagement.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly LibraryContext _context;
        public CategoryRepository(LibraryContext libraryContext) { 
            this._context = libraryContext;
        }

        public List<Category> GetAllCategories()
        {
            return _context.Categoryies.ToList();
        }

        public Category? GetCategoryById(Guid id)
        {
            return _context.Categoryies.FirstOrDefault(d => d.CategoryId == id);
        }

        public Category AddCategory(Category category)
        {
            _context.Categoryies.Add(category);
            _context.SaveChanges();

            return category;
        }

        public Category UpdateCategory(Category category)
        {
            _context.Categoryies.Update(category);
            _context.SaveChanges();

            return category;
        }

    }
}
