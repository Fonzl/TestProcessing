﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.QuestDto
{
    public class QuestDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public CategoryTasksDto.CategoryTasksDto CategoryTasks { get; set; }
        public List<TestDto.DetailsTestDto> Tests { get; set; }
        public List<AnswerDto.AnswerDto> Answers { get; set; }
    }
}