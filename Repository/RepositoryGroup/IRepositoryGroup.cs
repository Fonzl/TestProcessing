
using DTO.GroupDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RepositoryGroup
{
    public interface IRepositoryGroup
    {
        List<GroupDto> GetTestList();
        DetailsGroupDto GetTestDto(long id);
        void Update(UpdateGroupDto dto);
        void Delete(long id);
        void Insert(CreateGroupDto dto);
    }
}
