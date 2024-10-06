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

        [MaxLength(15, ErrorMessage = "BloggerName must be 10 characters or less")]
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string LastName { get; set; }
        public long Id_Group { get; set; }


    }
}
