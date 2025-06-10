using DTO.ResultTestDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Repository.RepositoryResultTest
{
    public interface IRepositoryResultTest
    {
        List<UserAttemptShort> userAttemptShorts(long groupId,long testId);

        decimal GetStatisticsDiscipline(ResultStatisticsDto dto);
        List<ResultOfAttemptsDTO> ResultStudentId(long studentId, long idDiscipline);// лист всех попыток теста ученика по дисциплине
 

        bool CheckingForAccess();// проверка есть ли не законченные тесты
        ResultOfAttemptsDTO InsertStudent(AddResultTestStudentDto dto);// финальный ответ студента
        ReturnAttemptDto? CheckingStudentResult(long testId,long studentId);
        long CreatResultAndAttempt(long testId, long studentId);
        List<VerifiedUserResponesDto> returnResultDetailsTrue(long idResulTest);
        void UpdateRespones (AddResultTestStudentDto dto);
        public List<VerifiedUserResponesDtoShort> returnResultDetailsFalse(long idResulTest);
        public DTO.GeneralDto.IsBoolDto TestBool(long IdResponse);
    }
}
