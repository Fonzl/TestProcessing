using DTO.ResultTestDto;
using Repository.RepositoryResultTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceResultTest
{
    public class ServiceResultTest(IRepositoryResultTest repo) : IServiceResultTest
    {
        public void CreateResultTest(CreateResultTestDto createResultTest)
        {
            repo.Insert(createResultTest);
        }

        public void DeleteResultTest(int id)
        {
            repo.Delete(id);
        }

        public List<ResultTestDto> GetAllResultTests()
        {
            return repo.GetResults();
        }

        public ResultTestDto GetResultTest(int id)
        {
           return repo.GetResult(id);
        }

        public void UpdateResultTest(UpdateResultTestDto updateResultTest)
        {
            repo.Update(updateResultTest);
            
        }
      
    }
}
