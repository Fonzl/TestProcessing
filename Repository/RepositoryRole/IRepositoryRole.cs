using DTO.RoleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RepositoryRole
{
    public interface IRepositoryRole
    {
        RoleDto Get(int id);
        List<RoleDto> GetAll();
        void Insert(CreateRoleDto addDto);
        void Update(UpdateRoleDto updateDto);
        void Delete(int id);
    }
}
