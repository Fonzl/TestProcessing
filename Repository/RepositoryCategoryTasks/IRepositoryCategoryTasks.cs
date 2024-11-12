
using DTO.CategoryTasksDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RepositoryCategoryTasks
{
    public interface IRepositoryCategoryTasks
    {
        CategoryTasksDto GetCategory(int id);
        List<CategoryTasksDto> GetAll();
        void Insert(CreateCategoryTasksDto dto);
        void Update(UpdateCategoryTasksDto dto);
        void Delete(long id);
    }
}
