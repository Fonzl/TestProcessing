﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DTO.UserDto
{
    public class LoginUserDto
    {
        [Required]
        public string Name {  get; set; }
        [Required]
        [RegularExpression(@"^(?=.*[0-9])(?=.*[A-Z])(?=.*[a-z]).{6,}$")]

        public string Password { get; set; }
    }   
}
