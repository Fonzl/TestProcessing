using DTO.CourseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RepositoryCourse
{
    public interface IRepositoryCourse
    {
        CourseDto Course(short id);
        List<CourseDto> CourseList();
        void Update(short id);
        void Delete(short id);
        void Insert(short id);
    }
}
