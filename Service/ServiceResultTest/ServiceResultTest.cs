using DTO.GroupDto;
using DTO.ResultTestDto;
using Repository.RepositoryResultTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceResultTest
{
    public class ServiceResultTest(IRepositoryResultTest repo) : IServiceResultTest
    {
        public ResultOfAttemptsDTO CreateResultTest(AddResultTestStudentDto createResultTest,long attemptId)
        {
           return repo.InsertStudent(createResultTest,attemptId);
        }

        public void DeleteResultTest(int id)
        {
            repo.Delete(id);
        }

        public List<ResultTestDto> GetAllResultTests()
        {
            return repo.GetResults();
        }
        public ResultTestDto GetResultTest(int id)
        {
           return repo.GetResult(id);
        }

        public decimal GetStatisticsDiscipline(ResultStatisticsDto dto)
        {
            return repo.GetStatisticsDiscipline(dto);
        }

        public List<ResultTestDto> ResultStudentId(long studentId)
        {
            return repo.ResultStudentId(studentId);
        }

        public List<VerifiedUserResponesDto> ReturnResultDetails(long idResultTest)
        {
            return repo.returnResultDetails(idResultTest);
        }

        public void UpdateResultTest(UpdateResultTestDto updateResultTest)
        {
            repo.Update(updateResultTest);
            
        }
       public long CreatResultAndAttempt(long testId, long studentId)
        {
            return repo.CreatResultAndAttempt(testId, studentId);
        }
    }
}
