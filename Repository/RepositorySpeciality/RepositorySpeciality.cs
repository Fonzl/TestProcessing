using Database;
using DTO.SpecialityDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RepositorySpeciality
{
    public class RepositorySpeciality(ApplicationContext context) : IRepositorySpeciality
    {
        public void Delete(short id)
        {
           
            var spec = context.Specialities.First(x => x.Id == id);
            context.Specialities.Remove(spec);
            context.SaveChanges();
        }

        public SpecialityDto GetSpeciality(short id)
        {
           var spec1 = context.Specialities.First(x=>x.Id == id);
            return (new SpecialityDto
            {
                Id = id,
                Name = spec1.Name,
            });

        }

        public List<SpecialityDto> GetSpecialties()
        {
            var listSpec = context.Specialities.ToList();
            var listAll = new List<SpecialityDto>();
            foreach (var spec in listSpec)
            {
                listAll.Add(new SpecialityDto
                {
                    Id = spec.Id,
                    Name = spec.Name,
                });

            }
            return listAll;
        }

        public void Insert(CreateSpecialityDto dto)
        {
            var spec = new Speciality
            {
                Name = dto.Name,
                Tests = context.Tests.Where(x => dto.Tests.Contains(x.Id)).ToList(),
            };
            context.Specialities.Add(spec);
            context.SaveChanges();
        }

        public void Update(UpdateSpecialityDto dto)
        {
            var spec = context.Specialities.First(x => x.Id == dto.Id);
            spec.Name = dto.Name;
            spec.Tests = context.Tests.Where(x => dto.Tests.Contains(x.Id)).ToList();
            context.Specialities.Update(spec);
            context.SaveChanges();
        }
    }
}
