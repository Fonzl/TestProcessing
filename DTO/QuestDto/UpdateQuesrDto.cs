using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.QuestDto
{
    public class UpdateQuesrDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public int  Id_CategoryTasks { get; set; }
        public List<long> Id_Tests { get; set; }
        public  List<long>Id_Answers { get; set; }
    }
}
