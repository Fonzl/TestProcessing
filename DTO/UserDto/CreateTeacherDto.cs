using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.UserDto
{
    public class CreateTeacherDto
    {
        public string FullName { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*[0-9])(?=.*[A-Z])(?=.*[a-z]).{6,}$")]
        public string Password { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*[0-9])(?=.*[A-Z])(?=.*[a-z]).{6,}$")]
        public string checkingPassword { get; set; }
        public List<int>? Disciplines { get; set; }
    }
}
