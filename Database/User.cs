using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class User
    {
        public long Id { get; set; }

        [Required]
        public string FullName { get; set; }
        [Required]
        public string Password { get; set; }
        public Group? Group { get; set; }
        public List<Discipline>? Disciplines { get; set; }
      

        public List<Test>? Tests { get; set; }
        public short RoleId { get; set; }
        [ForeignKey("RoleId")]
        public Role Role { get; set; }
    }
}
