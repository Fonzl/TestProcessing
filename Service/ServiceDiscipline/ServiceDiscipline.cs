using DTO.DisciplineDto;
using Repository.RepositoryDiscipline;
using Repository.RepositoryUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceDiscipline
{
    public class ServiceDiscipline(IRepositoryDiscipline repo,IRepositoryUser repositoryUser) : IServiceDiscipline
    {
        public void CreateDiscipline(CreateDisciplineDto discipline)
        {
            repo.Insert(discipline);
        }

        public void DeleteDiscipline(int id)
        {
           repo.Delete(id);
        }

        public List<DisciplineDto> GetAllDiscipline()
        {
            return repo.GetAll();
        }

        public DetailsDisciplineDto GetDiscipline(int id)
        {
           return repo.Get(id);
        }

        public List<DisciplineDto> StudentGetDiscipline(long id)
        {
            return repo.StudentGet(id);
        }

        public List<DisciplineDto> StudentGetProfil(long id)
        {
            return repo.StudentGetProfil(id);
        }

        public List<DisciplineDto> TeacherGetDiscipline(long id)
        {
          return repo.TeacherGet(id);

        }

        public List<DisciplineDto> TheacherStudentGetProfil(long idStudent, long idTeacher)
        {
            if (repositoryUser.GetTeacher(idTeacher) != null)
            {
                return repo.TheacherStudentGetProfil(idStudent, idTeacher);
            }
            return repo.StudentGetProfil(idStudent);
           
        }

        public void UpdateDiscipline(UpdateDisciplineDto discipline)
        {
            repo.Update(discipline);
        }
    }
}
