using DTO.GroupDto;
using Repository.RepositoryGroup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceGroup
{
    public class ServiceGroup(IRepositoryGroup repo) : IServiceGroup
    {
        public void CreateGroup(CreateGroupDto group)
        {
            repo.Insert(group);
        }

        public void DeleteGroup(int id)
        {
            repo.Delete(id);
        }

        public List<GroupDto> GetAllGroups()
        {
           return repo.GetGroupList();
        }

        public DetailsGroupDto GetGroup(int id)
        {
           return repo.GetGroupDto(id);
        }

        public DetailsGroupDto GetGroupUser(long userId)
        {
           return repo.GetGroupUser(userId);
        }

        public void UpdateGroup(UpdateGroupDto group)
        {
            repo.Update(group);
        }
    }
}
