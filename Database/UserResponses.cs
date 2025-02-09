using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class UserResponses
    {

        public long Id { get; set; }
        public decimal Result { get; set; }
        public string EvaluationName { get; set; }
        public string ListUserResponses { get; set; }
   
        public ResultTest ResultTest { get; set; }
    }
}
