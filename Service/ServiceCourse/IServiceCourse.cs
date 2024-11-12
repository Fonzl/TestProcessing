using DTO.CourseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceCourse
{
    public interface IServiceCourse
    {
        CourseDto GetCourse(short id);
        List<CourseDto> GetAllCours();
        void DeleteCourse(short id);
        void UpdateCourse(short id);
        void CreateCourse(short number);
    }
}
