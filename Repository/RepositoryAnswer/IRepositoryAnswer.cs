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
        AnswerDto Answer(long id);
        List<StudentAnsewerDto> Answers(long id);
        void InsertList(List<CreateAnswerDto> list);
        void Insert(CreateAnswerDto dto);
        void Update(UpdateAnswerDto dto);
        void Delete(long id);
    }
}
