using DTO.ResultTestDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RepositoryResultTest
{
    public interface IRepositoryResultTest
    {
        List<ResultTestDto> GetResults();
        ResultTestDto GetResult(long id);
        decimal GetStatisticsDiscipline(ResultStatisticsDto dto);
        List<ResultTestDto> ResultStudentId(long studentId);
        void Update(UpdateResultTestDto dto);
        void Delete(int id);
        ReturnResultAndRespone InsertStudent(AddResultTestStudentDto dto);
        ReturnResultAndRespone returnResultDetails(long idResulTest);
    }
}
