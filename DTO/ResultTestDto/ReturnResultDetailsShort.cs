using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ResultTestDto
{
    public class ReturnResultDetailsShort
    {
        public int numberOfIncorrect { get; set; }
        public int numberOfCorrect { get; set; }
        public List<VerifiedUserResponesDtoShort> verifiedUserRespones { get; set; }
    }
}
