using Database;
using DTO.GroupDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceGroup
{
    public interface IServiceGroup
    {
        DetailsGroupDto GetGroup(int id);
        DetailsGroupDto GetGroupUser(long userId);
        List<GroupDto> GetDisciplineGroupList(long idDiscipline);
        List<GroupDto> GetAllGroups();
       void DeleteGroup(int id);
       void UpdateGroup(UpdateGroupDto group);
       void CreateGroup(CreateGroupDto group);
    }
}
