using Microsoft.Extensions.Options;
using Orders.Api.models.dtos;

namespace Orders.Api.services
{
    public class StudentService : IStudentService
    {
        private readonly IOptions<StudentInfo> _optionStudentInfo;

        public StudentService(IOptions<StudentInfo> optionStudentInfo)
        {
            _optionStudentInfo = optionStudentInfo;
        }

        

        public StudentInfo GetStudentInfo()
        {
            return new StudentInfo
            {
                Name = _optionStudentInfo.Value.Name,
                Password = _optionStudentInfo.Value.Password
            };
        }
    }
}