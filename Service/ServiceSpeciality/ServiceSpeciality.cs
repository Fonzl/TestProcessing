using DTO.SpecialityDto;
using Repository.RepositorySpeciality;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceSpeciality
{
    public class ServiceSpeciality(IRepositorySpeciality repo) : IServiceSpeciality
    {
        public void CreateSpeciality(CreateSpecialityDto createSpeciality)
        {
            repo.Insert(createSpeciality);
        }

        public void DeleteSpeciality(short id)
        {
            repo.Delete(id);
        }

        public List<SpecialityDto> GetAllSpecialties()
        {
            return repo.GetSpecialties();
        }

        public SpecialityDto GetSpeciality(short id)
        {
            return repo.GetSpeciality(id);
        }

        public void UpdatSpeciality(UpdateSpecialityDto updateSpeciality)
        {
            repo.Update(updateSpeciality);
        }
    }
}
