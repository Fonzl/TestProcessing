using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ResultTestDto
{
    public class ReturnResultDetails
    {
      public  int numberOfIncorrect {  get; set; }
        public int numberOfCorrect { get; set; }
       public List<VerifiedUserResponesDto> verifiedUserRespones { get; set; }

    }
}
