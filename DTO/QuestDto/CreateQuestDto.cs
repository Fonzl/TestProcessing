﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.QuestDto
{
    public class CreateQuestDto
    {
     
        public string Name { get; set; }
        public string Info { get; set; }
        public int CategoryTasksId { get; set; }

    }
}