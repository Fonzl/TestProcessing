﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ResultTestDto
{
    public class CreateResultTestDto
    {

        public long UserId { get; set; }
        public long TestId { get; set; }
        public decimal Result { get; set; }
    }
}
