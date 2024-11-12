﻿using DTO.DisciplineDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceDiscipline
{
   public interface IServiceDiscipline
    {
        DetailsDisciplineDto GetDiscipline(int id);
        List<DisciplineDto> GetAllDiscipline();
        void DeleteDiscipline(int id);
        void UpdateDiscipline(UpdateDisciplineDto discipline);
        void CreateDiscipline(CreateDisciplineDto discipline);
    }
}