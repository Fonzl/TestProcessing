using DTO.DisciplineDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceDiscipline
{
   public interface IServiceDiscipline
    {
        DetailsDisciplineDto GetDiscipline(int id);

        List<DisciplineDto> GetAllDiscipline();
        List<DisciplineDto> TeacherGetDiscipline(long id);
        List<DisciplineDto> StudentGetDiscipline(long id);
        void DeleteDiscipline(int id);
        List<DisciplineDto> StudentGetProfil(long id);
        List<DisciplineDto> TheacherStudentGetProfil(long idStudent,long idTeacher);
        void UpdateDiscipline(UpdateDisciplineDto discipline);
        void CreateDiscipline(CreateDisciplineDto discipline);

    }
}
