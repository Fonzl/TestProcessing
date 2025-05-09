using DTO.DirectionDto;
using Repository.RepositoryDirection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceDirection
{
    public class ServiceDirection(IRepositoryDirection repo) : IServiceDirection
    {
        public void CreateDirection(CreateDirectionDto createAnswerDto)
        {
             repo.Insert(createAnswerDto);
        }

        public void DeleteDirections(int id)
        {
           repo.Delete(id);
        }

        public DirectionDto GetDirection(int id)
        {
           return repo.GetDirection(id);
        }

        public List<DirectionDto> GetDirections()
        {
            return repo.GetAll();
        }

        public ReturnScheduleDirection GetDirectionsShedule(int coursId, int directionId)
        {
           return 
                repo.GetDirectionsShedule(coursId, directionId);
        }

        public List<ReturnScheduleDirectionShortDto> GetDirectionsSheduleShort()
        {
            return repo.GetDirectionsSheduleShort();
        }

        public void UpdateDirection(UpdateDirectionDto updateAnswerDto)
        {
            repo.Update(updateAnswerDto);
        }

        public void UpdateSchedule(UpdateScheduleDirectionDto updateSheduleDirectionDto)
        {
            repo.UpdateShedule(updateSheduleDirectionDto);
        }
    }
}
