using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class Test
    {
        public long Id {  get; set; }
        public string Name { get; set; }
        public string InfoTest { get; set; }
        public Course Course { get; set; }
        public List<Speciality>Specialties { get; set; }
        public List<Quest> Quests { get; set; }
        public Discipline Discipline { get; set; }
    }
}
