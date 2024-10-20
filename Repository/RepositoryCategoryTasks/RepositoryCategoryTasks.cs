using Database;
using DTO.CategoryTasksDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RepositoryCategoryTasks
{
    internal class RepositoryCategoryTasks(ApplicationContext context) : IRepositoryCategoryTasks
    {
        private readonly ApplicationContext _context = context;
        private DbSet<CategoryTasks> _category = context.Set<CategoryTasks>();

        public void Delete(long id)
        {
           var category = _category.SingleOrDefault(c => c.Id == id);
            if (category == null) return;
            _category.Remove(category);
            context.SaveChanges();

        }

        public void Insert(CreateCategoryTasksDto dto)
        {
            var category = new CategoryTasks
            {
                Name = dto.Name
            };
            _category.Add(category);
            _context.SaveChanges();
        }

        public void Update(UpdateCategoryTasksDto dto)
        {
            var category = _category.SingleOrDefault(x => x.Id == dto.Id);
            if (category == null) return;
            category.Name = dto.Name;
            _category.Update(category);
            _context.SaveChanges();
          
        }
    }
}
