using DTO.RoleDto;
using Repository.RepositoryRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceRole
{
    public class ServiceRole(IRepositoryRole repo) : IServiceRole
    {
        public void CreateRole(CreateRoleDto createRole)
        {
            repo.Insert(createRole);
        }

        public void DeleteRole(short id)
        {
            repo.Delete(id);
        }

        public List<RoleDto> GetAllRoles()
        {
           return repo.GetAll();
        }

        public RoleDto GetRole(short id)
        {
           return repo.Get(id);
        }

        public void UpdateRole(UpdateRoleDto updateRole)
        {
            repo.Update(updateRole);
        }
    }
}
