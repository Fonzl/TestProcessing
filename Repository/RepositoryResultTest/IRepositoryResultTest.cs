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
        void Update(UpdateResultTestDto dto);
        void Delete(int id);
        void InsertStudent(AddResultTestStudentDto dto);
    }
}
