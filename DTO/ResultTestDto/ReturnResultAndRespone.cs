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
        public List<VerifiedUserRespones> Respones { get; set; }
    }
}
