﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class Speciality
    {
        public short Id {  get; set; }
        public string Name { get; set; }
        public List<Test> Tests { get; set; }
    }
}
