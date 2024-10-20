﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.TestDto
{
    public class DetailsTestDto : TestDto
    {
        public string InfoTest { get; set; }    
        public CourseDto.CourseDto Course { get; set; }
        public List<SpecialityDto.SpecialityDto> Specialties { get; set; }
        public List<QuestDto.QuestDto> Quests { get; set; }
        public DisciplineDto.DetailsDisciplineDto? Discipline { get; set; }
    }
}
