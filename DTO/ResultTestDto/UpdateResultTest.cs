using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ResultTestDto
{
    public class UpdateResultTest
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long TestId { get; set; }
        public decimal Result { get; set; }
    }
}
