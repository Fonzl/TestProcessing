using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;

namespace Repository.RepositoryCourse
{
    public class RepositoryCourse(ApplicationContext context) : IRepositoryCourse
    {
        public void Delete(short id)
        {
            var course = context.Courses.First(c => c.Id == id);
            context.Courses.Remove(course);
            context.SaveChanges();
        }

        public void Insert(short id)
        {
            context.Courses.Add(new Course() { Id = id });
            context.SaveChanges();
        }

        public void Update(short id)
        {
            var course = context.Courses.First(c => c.Id == id);
            course.Id = id;
            context.Courses.Update(course);
            context.SaveChanges() ;
        }
    }
}
