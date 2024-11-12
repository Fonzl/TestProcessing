
using DTO.CategoryTasksDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceCategoryTasks
{
   public interface IServiceCategoryTasks
    {
        CategoryTasksDto GetCategory(int id);
        List<CategoryTasksDto> GetCategoryTasks();
        void DeleteCategory(int id);
        void UpdateCategory(UpdateCategoryTasksDto updateAnswerDto);
        void CreateCategory(CreateCategoryTasksDto createAnswerDto);
    }
}
