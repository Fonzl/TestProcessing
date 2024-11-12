using DTO.CategoryTasksDto;
using Repository.RepositoryCategoryTasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceCategoryTasks
{
    public class ServiceCategoryTasks(IRepositoryCategoryTasks repo) : IServiceCategoryTasks
    {
        public void CreateCategory(CreateCategoryTasksDto createAnswerDto)
        {
            repo.Insert(createAnswerDto);
        }

        public void DeleteCategory(int id)
        {
           repo.Delete(id);
        }

        public CategoryTasksDto GetCategory(int id)
        {
            return repo.GetCategory(id);
        }

        public List<CategoryTasksDto> GetCategoryTasks()
        {
            return repo.GetAll();
        }

        public void UpdateCategory(UpdateCategoryTasksDto updateAnswerDto)
        {
           repo.Update(updateAnswerDto);
        }
    }
}
