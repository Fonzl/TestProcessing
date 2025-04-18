﻿using Database;

using DTO.TestDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceTest
{
    public interface IServiceTest
    {
        DetailsTestDto GetTest(long id);
        List<TestDto> GetTests();
        void DeleteTest(long id);
        void UpdateTest(UpdateTestDto updateTestDto);
        long CreateTest(CreateTestDto createTestDto);
        //public List<TestDto> GetTestsListStudent(long id);
        public List<TestDto> GetTestsListDiscipline(long disciplineId, long IdUsser);
        DetailsTestDto GetTestStudentDto(long testId, long IdUsser);

    }
}
