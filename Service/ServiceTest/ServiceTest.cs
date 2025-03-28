using DTO.TestDto;
using Repository.RepositoryTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceTest
{
    public class ServiceTest(IRepositoryTest repo) : IServiceTest
    {
        public void CreateTest(CreateTestDto createTestDto)
        {
            repo.Insert(createTestDto);
        }

        public void DeleteTest(long id)
        {
            repo.Delete(id);
        }

        public DetailsTestDto GetTest(long id)
        {
            return repo.GetTestDto(id);
        }

        public List<TestDto> GetTests()
        {
            return repo.GetTestList();
        }

        public void UpdateTest(UpdateTestDto updateTestDto)
        {
            repo.Update(updateTestDto);
        }
        //public List<TestDto> GetTestsListStudent(long id)
        //{
          

            
        //    return repo.GetTestStudent( id);
        //}

        public List<TestDto> GetTestsListDiscipline(long disciplineId,long IdUsser)
        {
            return repo.GetTestDiscipline( disciplineId,  IdUsser);
        }

        public DetailsTestDto GetTestStudentDto(long testId, long IdUsser)
        {
            return repo.GetTestStudentDto( testId, IdUsser);
        }
    }
}
