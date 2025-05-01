using DTO.QuestDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ResultTestDto
{
    public class VerifiedUserResponesDto : VerifiedUserResponesDtoShort

    {
        
        public List<AnswerDto.AnswerVerfiedDto> UserRespones { get; set; }
        


    }
}
