﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.SpecialityDto
{
    public class CreateSpecialityDto
    {
        public short Id { get; set; }
        public string Name { get; set; }
        public List<long> Tests { get; set; }
    }
}