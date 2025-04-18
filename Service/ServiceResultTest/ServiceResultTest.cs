﻿using DTO.GroupDto;
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
        public ResultOfAttemptsDTO CreateResultTest(AddResultTestStudentDto createResultTest)
        {
           return repo.InsertStudent(createResultTest);
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

        public List<ResultTestDto> ResultStudentId(long studentId, long idDiscipline)
        {
            return repo.ResultStudentId(studentId,  idDiscipline);
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

        public ReturnAttemptDto? CheckingStudentResult(long testId, long studentId)
        { 
         return  repo.CheckingStudentResult(testId, studentId);
        }
            public void UpdateRespones(AddResultTestStudentDto dto)
        {
            repo.UpdateRespones(dto);
        }
    }
}
