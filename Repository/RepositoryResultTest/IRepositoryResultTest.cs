﻿using DTO.ResultTestDto;
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
        List<ResultTestDto> ResultStudentId(long studentId, long idDiscipline);
        void Update(UpdateResultTestDto dto);
        void Delete(int id);
        bool CheckingForAccess();
        ResultOfAttemptsDTO InsertStudent(AddResultTestStudentDto dto);
        ReturnAttemptDto? CheckingStudentResult(long testId,long studentId);
        long CreatResultAndAttempt(long testId, long studentId);
        List<VerifiedUserResponesDto> returnResultDetails(long idResulTest);
        void UpdateRespones (AddResultTestStudentDto dto);
    }
}
