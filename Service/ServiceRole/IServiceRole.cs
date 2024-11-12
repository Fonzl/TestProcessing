using DTO.ResultTestDto;
using DTO.RoleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceRole
{
    public interface IServiceRole
    {
        RoleDto GetRole(short id);
        List<RoleDto> GetAllRoles();
        void DeleteRole(short id);
        void UpdateRole(UpdateRoleDto updateRole);
        void CreateRole(CreateRoleDto createRole);
    }
}
