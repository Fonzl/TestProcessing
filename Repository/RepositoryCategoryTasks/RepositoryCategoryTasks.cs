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
    public class RepositoryCategoryTasks(ApplicationContext context) : IRepositoryCategoryTasks
    {

     

        public void Delete(long id)
        {
           var category = context.CategoryTasks.SingleOrDefault(c => c.Id == id);
            if (category == null) return;
            context.CategoryTasks.Remove(category);
            context.SaveChanges();

        }

        public List<CategoryTasksDto> GetAll()
        {
           var list = context.CategoryTasks.ToList();
           var categoryTasks = new List<CategoryTasksDto>();
            foreach (var task in list)
            {
                categoryTasks.Add(new CategoryTasksDto
                {
                    Id = task.Id,
                    Name = task.Name,
                });
            }
            return categoryTasks;
        }

        public CategoryTasksDto GetCategory(int id)
        {
           var category = context.CategoryTasks.First(c => c.Id == id);
            return new CategoryTasksDto()
            {
                Id = category.Id,
                Name = category.Name,
            };
        }

        public void Insert(CreateCategoryTasksDto dto)
        {
            var category = new CategoryTasks
            {
                Name = dto.Name
            };
            context.CategoryTasks.Add(category);
            context.SaveChanges();
        }
        public void Update(UpdateCategoryTasksDto dto)
        {
            var category = context.CategoryTasks.SingleOrDefault(x => x.Id == dto.Id);
            if (category == null) return;
            category.Name = dto.Name;
            context.CategoryTasks.Update(category);
            context.SaveChanges();
          
        }
    }
}
