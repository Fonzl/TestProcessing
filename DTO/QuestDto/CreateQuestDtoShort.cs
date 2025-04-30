using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.QuestDto
{
    public class CreateQuestDtoShort
    {
        public string Name { get; set; }
        public string Info { get; set; }
        public short CategoryTaskId { get; set; }
        public List<long> Tests { get; set; }
    }
}
