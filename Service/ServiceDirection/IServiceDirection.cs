using DTO.DirectionDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceDirection
{
    public interface IServiceDirection
    {
        DirectionDto GetDirection(int id);
        List<DirectionDto> GetDirections();
        void DeleteDirections(int id);
        void UpdateDirection(UpdateDirectionDto updateAnswerDto);
        void CreatShedule(CreatShedule dto);
        public List<ReturnScheduleDirectionShortDto> GetDirectionsSheduleShort();
        void UpdateSchedule(UpdateScheduleDirectionDto updateSheduleDirectionDto);
         ReturnScheduleDirection GetDirectionsShedule(int coursId, int directionId);
        void CreateDirection(CreateDirectionDto createAnswerDto);
    }
}
