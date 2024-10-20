using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class User
    {
        public long Id { get; set; }
        [Required]
        [MaxLength(15, ErrorMessage = "Максимальное количество знаков 15")]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(15, ErrorMessage = "Максимальное количество знаков 15")]
        public string Surname { get; set; }
        [Required]
        [MaxLength(30, ErrorMessage = "Максимальное количество знаков 15")]
        public string LastName { get; set; }
        public Group? Group { get; set; }
        public List<Discipline> Disciplines { get; set; }
        public Role Role {  get; set; }


    }
}
