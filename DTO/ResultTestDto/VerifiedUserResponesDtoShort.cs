﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ResultTestDto
{
    public class VerifiedUserResponesDtoShort
    {
        public DTO.QuestDto.QuestDto QuestDto { get; set; }
        public bool IsCorrectQuest { get; set; }
        public DTO.CategoryTasksDto.CategoryTasksDto CategoryTasksDto { get; set; }
    }
}
