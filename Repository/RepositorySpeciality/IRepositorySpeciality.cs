using DTO.ResultTestDto;
using DTO.SpecialityDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RepositorySpeciality
{
    public interface IRepositorySpeciality
    {
        List<SpecialityDto> GetSpecialties();
        SpecialityDto GetSpeciality(short id);
        void Update(UpdateSpecialityDto dto);
        void Delete(short id);
        void Insert(CreateSpecialityDto dto);
    }
}
