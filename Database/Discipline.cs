﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class Discipline
    {
        public int Id { get; set; }
        public string Name { get; set; }
    
        public List<Test>? Tests { get; set; }
        public List<User> Users { get; set; }
        public List<Schedule>? Schedules { get; set; }
    }
}
