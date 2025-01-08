using DTO.GroupDto;
using DTO.ResultTestDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceResultTest
{
    public interface IServiceResultTest
    {
        ResultTestDto GetResultTest(int id);
        List<ResultTestDto> GetAllResultTests();
        decimal GetStatisticsDiscipline(ResultStatisticsDto dto);
        List<ResultTestDto> ResultStudentId(long studentId);
        void DeleteResultTest(int id);
        void UpdateResultTest(UpdateResultTestDto updateResultTest);
        void CreateResultTest(AddResultTestStudentDto createResultTest);
    }
}
