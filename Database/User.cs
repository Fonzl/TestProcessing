using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public short RoleId { get; set; }
        [ForeignKey("RoleId")]
        public Role Role { get; set; }
        public string Login{ get; set; }

    }
}
