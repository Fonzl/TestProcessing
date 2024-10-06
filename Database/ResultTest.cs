using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class ResultTest
    {
        public long Id { get; set; }
        public User User { get; set; }
        public Test Test { get; set; }
        public decimal Result {  get; set; } 
    }
}
