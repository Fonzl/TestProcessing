using DTO.DisciplineDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RepositoryDiscipline
{
     public interface IRepositoryDiscipline
    {
        List<DisciplineDto> GetAll();
        List<DisciplineDto> TeacherGet(int id);
        List<DisciplineDto> StudentGet(int id);
        DetailsDisciplineDto Get(int id);
        void Insert(CreateDisciplineDto dto);
        void Update(UpdateDisciplineDto dto);
        void Delete(int id);
    }
}
