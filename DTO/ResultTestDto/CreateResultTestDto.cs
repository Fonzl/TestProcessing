﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ResultTestDto
{
    public class CreateResultTestDto
    {

       
        public long TestId { get; set; }
        public List<UserRespons> UserResponesTest { get; set; }
       public long idResult {  get; set; }
    }
}
