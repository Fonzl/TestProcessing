using DTO.TestDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RepositoryTest
{
    public interface IRepositoryTest
    {   
        List<TestDto> GetTestList();
        DetailsTestDto GetTestDto (long id);
        void Update(UpdateTestDto dto);
        void Delete(long id);
        void Insert(CreateTestDto dto);
        public List<TestDto> GetTestUser(long id);


    }
}
