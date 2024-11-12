using DTO.RoleDto;
using DTO.SpecialityDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceSpeciality
{
    public interface IServiceSpeciality 
    {
       SpecialityDto GetSpeciality(short id);
       List<SpecialityDto> GetAllSpecialties();
       void DeleteSpeciality(short id);
       void UpdatSpeciality(UpdateSpecialityDto updateSpeciality);
       void CreateSpeciality(CreateSpecialityDto createSpeciality);
    }
}
