using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
   public class Group
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDateOfTraining { get; set; }
        public DateTime EndOfTraining { get; set; }
        public  Speciality Speciality { get; set; }
        public List<User> Users { get; set; }
        public Course Course { get; set; }
    }
}
