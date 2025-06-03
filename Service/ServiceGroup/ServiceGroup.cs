using DTO.GroupDto;
using Repository.RepositoryGroup;
using Repository.RepositoryUser;
using Service.ServiceUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceGroup
{
    public class ServiceGroup(IRepositoryGroup repo,IServiceUser serviceUser) : IServiceGroup
    {
        public void CreateGroup(CreateGroupDto group)
        {
            repo.Insert(group);
        }

        public void DeleteGroup(int id)
        {
            var users = repo.Delete(id);
            if(users == null)
            {
                return;
            }
            foreach(var user in users)
            {
               serviceUser.DeleteUser(user.Id);
            }

        }

        public List<GroupDto> GetAllGroups()
        {
           return repo.GetGroupList();
        }

        public List<GroupDto> GetDisciplineGroupList(long idDiscipline)
        {
            return repo.GetDisciplineGroupList(idDiscipline);
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
