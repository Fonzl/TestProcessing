using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ResultTestDto
{
    public class ResultOfAttemptsDTO//Класс для возрата краткой инфориации о попытках 
    {
        public long IdAttempts { get; set; } 
        public decimal Result { get; set; }
        public string? EvaluationName { get; set; }
        public int Attempts { get; set; }
    }
}
