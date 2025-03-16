
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
        List<GroupDto> GetGroupList();
        List<GroupDto> GetDisciplineGroupList(long idDiscipline);
        DetailsGroupDto GetGroupDto(long id);
        DetailsGroupDto GetGroupUser(long userId);
        void Update(UpdateGroupDto dto);
        void Delete(long id);
        void Insert(CreateGroupDto dto);
     
    }
}
