using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.TestDto
{
    public class EvaluationDto//Это класс оценки.Оценка в тестах может вестить по разномую.Поэтому в тесте будет храниться  лист таких классов виде стринга. 
    {
        public string EvaluationName { get; set; }
        public int Percent {  get; set; }
    }
}
