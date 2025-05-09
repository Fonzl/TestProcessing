using DTO.DirectionDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Repository.RepositoryDirection
{
    public interface IRepositoryDirection
    {
        DirectionDto GetDirection(int id);
        List<DirectionDto> GetAll();
        void Insert(CreateDirectionDto dto);
        void Update(UpdateDirectionDto dto);
        void UpdateShedule(UpdateScheduleDirectionDto dto);
        void GetShedule(UpdateScheduleDirectionDto dto);
        public List<ReturnScheduleDirectionShortDto> GetDirectionsSheduleShort();
       ReturnScheduleDirection GetDirectionsShedule(int coursId,int directionId);
        void Delete(int id);
    }
}
