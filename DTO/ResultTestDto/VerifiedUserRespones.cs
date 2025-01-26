using DTO.QuestDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ResultTestDto
{
    public class VerifiedUserRespones

    {
        public DTO.QuestDto.QuestDto QuestDto { get; set; }
        public List<string> UserRespones { get; set; }
        public bool IsCorrectAnswer { get; set; }
    }
}
