using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ResultTestDto
{
    public class ReturnResultAndRespone : ResultOfAttemptsDTO// Класс с подробной информацией о попытке 
    {
       
    
        public List<VerifiedUserResponesDto> ListRespones { get; set; }
       

    }
}
