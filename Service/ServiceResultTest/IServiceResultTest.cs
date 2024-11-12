using DTO.ResultTestDto;
using DTO.ResultTestDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceResultTest
{
    public interface IServiceResultTest
    {
        ResultTestDto GetResultTest(int id);
        List<ResultTestDto> GetAllResultTests();
        void DeleteResultTest(int id);
        void UpdateResultTest(UpdateResultTestDto updateResultTest);
        void CreateResultTest(CreateResultTestDto createResultTest);
    }
}
