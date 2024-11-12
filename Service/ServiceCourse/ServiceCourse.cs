using DTO.CourseDto;
using Repository.RepositoryCourse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceCourse
{
    public class ServiceCourse(IRepositoryCourse repo) : IServiceCourse
    {
        public void CreateCourse(short number)
        {
            repo.Insert(number);
        }

        public void DeleteCourse(short id)
        {
            repo.Delete(id);
        }

        public List<CourseDto> GetAllCours()
        {
            return repo.CourseList();
        }

        public CourseDto GetCourse(short id)
        {
            return repo.Course(id);
        }

        public void UpdateCourse(short id)
        {
            repo.Update(id);
        }
    }
}
