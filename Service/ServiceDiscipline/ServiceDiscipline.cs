using DTO.DisciplineDto;
using Repository.RepositoryDiscipline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceDiscipline
{
    public class ServiceDiscipline(IRepositoryDiscipline repo) : IServiceDiscipline
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

        public void UpdateDiscipline(UpdateDisciplineDto discipline)
        {
            repo.Update(discipline);
        }
    }
}
