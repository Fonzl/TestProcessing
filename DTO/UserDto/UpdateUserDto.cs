﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.UserDto
{
    public class UpdateUserDto
    {
        public long Id { get; set; }
       public string FullName { get; set; }
        public int? Group { get; set; }
        public List<int>? Disciplines { get; set; }
        public short Role { get; set; }
    }
}
