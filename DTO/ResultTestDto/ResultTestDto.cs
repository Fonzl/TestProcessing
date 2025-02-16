using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ResultTestDto
{
    public class ResultTestDto
    {
        public long Id { get; set; }
        public UserDto.StudentUserDto User { get; set; }
        public TestDto.DetailsTestDto Test { get; set; }
        public List<ResultOfAttemptsDTO> Result { get; set; }
        public string? EvaluationName { get; set; }
    }
}
