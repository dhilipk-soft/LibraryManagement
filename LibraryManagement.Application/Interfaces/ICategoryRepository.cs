using LibraryManagement.Model.Entities;

namespace LibraryManagement.Application.Interfaces
{
    public interface ICategoryRepository
    {
        public List<Category> GetAllCategories();
        public Category? GetCategoryById(Guid id);
        
        public Category AddCategory(Category category);

        public Category UpdateCategory(Category category);
    }
}
