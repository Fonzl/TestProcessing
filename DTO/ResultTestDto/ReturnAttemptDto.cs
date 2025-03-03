using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ResultTestDto
{
    public class ReturnAttemptDto : CreateResultTestDto
    {
        public long? Second { get; set; }
        public long? Minutes { get; set; }
        public long IdResult { get; set; }
    }
}
