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
           return repo.GetTestList();
        }

        public DetailsGroupDto GetGroup(int id)
        {
           return repo.GetTestDto(id);
        }

        public void UpdateGroup(UpdateGroupDto group)
        {
            repo.Update(group);
        }
    }
}
