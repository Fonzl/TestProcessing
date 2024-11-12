using Database;
using DTO.RoleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RepositoryRole
{
    public class RepositoryRole(ApplicationContext context) : IRepositoryRole
    {
        public void Delete(int id)
        {
            Role role = context.Roles.First(x => x.Id == id);
            context.Roles.Remove(role);
            context.SaveChanges();
        }

        public RoleDto Get(int id)
        {
            Role role = context.Roles.First(x => x.Id == id);
            return new RoleDto()
            {
                Id = role.Id,
                Name = role.Name
            };
        }

        public List<RoleDto> GetAll()
        {
            return context.Roles.Select(role => new RoleDto()
            {
                Id = role.Id,
                Name = role.Name
            }).ToList();
        }

        public void Insert(CreateRoleDto addDto)
        {
            context.Roles.Add(new Role { Name = addDto.Name });
            context.SaveChanges();
        }

        public void Update(UpdateRoleDto updateDto)
        {
            Role role = context.Roles.First(x => x.Id == updateDto.Id);
            role.Name = updateDto.Name;
            context.Roles.Update(role);
            context.SaveChanges();
        }
    }
}
