using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ResultTestDto
{
    public class ResultOfAttemptsDTO//Класс для возрата краткой инфориации о попытках 
    {
        public long IdUserRespones { get; set; } 
        public decimal Result { get; set; }
        public string? EvaluationName { get; set; }
        public int Attempts { get; set; }
        public bool IsChek {  get; set; }
        public string NameTest { get; set; }
        public DateTime DateFinish { get; set; }
    }
}
