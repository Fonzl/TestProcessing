﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.QuestDto
{
    public class ChekAnswerQuest
    {
        public CategoryTasksDto.CategoryTasksDto CategoryTasks { get; set; }
        public List<AnswerDto.AnswerDto> Answers { get; set; }

    }
}
