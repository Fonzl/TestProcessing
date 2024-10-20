using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.AnswerDto;

namespace Repository.RepositoryAnswer
{
    public interface IRepositoryAnswer
    {
        List<StudentAnsewerDto> Answers(long id);
        void Insert(CreateAnswerDto dto);
        void Update(UpdateAnswerDto dto);
        void Delete(long id);
    }
}
