﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.UserDto
{
    public class UserDto : ShortUserDto
    {

        public RoleDto.RoleDto Role { get; set; }
    }
}
