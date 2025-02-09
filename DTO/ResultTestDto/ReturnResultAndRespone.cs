using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ResultTestDto
{
    public class ReturnResultAndRespone
    {
       
        public decimal Result { get; set; }
        public string? EvaluationName { get; set; }
        public List<VerifiedUserResponesDto> ListRespones { get; set; }
        public int Attempts { get; set; }

    }
}
