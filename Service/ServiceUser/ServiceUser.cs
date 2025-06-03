using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DTO.UserDto;
using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;
using Repository.RepositoryUser;

namespace Service.ServiceUser
{
    public class ServiceUser(IRepositoryUser repo) : IServiceUser
    {

        public string CreateStudent(CreateStudentDto user)
        {


           
           return repo.InsertStudent(user, Transliterate(user.FullName));
        }

        public string CreateTeacher(CreateTeacherDto user)
        {
          return  repo.InsertTeacher(user,Transliterate(user.FullName));
        }

        public void DeleteUser(long id)
        {
            repo.Delete(id);
        }

        public List<StudentAttemptResultDto> GetStudentAttempt(long idGroup, long idTest)
        {
            return repo.GetStudentAttempt(idGroup, idTest);
        }

        public List<ShortUserDto> GetStudentIdGroup(long idGroup)
        {
            return repo.GetStudentIdGroup(idGroup);
        }

        public TeacherUserDto GetTeacher(long id)
        {
            return repo.GetTeacher(id);
        }

        public StudentUserDto GetUser(long id)
        {
            return repo.GetUser(id);
        }

        public List<ShortUserDto> GetUsers(short idRole)
        {
            return repo.GetUsers(idRole);
        }

        public ReturnLoginUser Login(string username, string password) // Находим пользователя по данным и делаем jwt token
        {

            var dto = repo.Login(username, password);
            if (dto == null)
            {
                

                return null;
            }
            var claims = new List<Claim> {
                new Claim("id", dto.Id.ToString()),
                new Claim(ClaimTypes.Role, dto.Role.Name) };
            var jwt = new JwtSecurityToken(
               issuer: AuthOptions.ISSUER,
               audience: AuthOptions.AUDIENCE,
               claims: claims,
               expires: DateTime.Now.Add(TimeSpan.FromDays(1)), 
               signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
               var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new ReturnLoginUser { jwt = token, IdUser = dto.Id, UserName = dto.FullName, RoleDto = dto.Role };
        }

        public void PasswordСhange(PasswordСhangeDto passwordСhangeDto)
        {
            repo.PasswordСhange(passwordСhangeDto);
        }

        public void UpdateStudent(UpdateStudentDto user)
        {
            repo.UpdateStudent(user);
        }

        public void UpdateTeacher(UpdateTeacherDto user)
        {
            repo.UpdateTeacher(user);
        }
        public string Transliterate(string text)
        {
            string login;
            do
            {
                var translitDict = new Dictionary<string, string>
                {
                    {"а", "a"}, {"б", "b"}, {"в", "v"}, {"г", "g"}, {"д", "d"}, {"е", "e"},
                    {"ё", "yo"}, {"ж", "zh"}, {"з", "z"}, {"и", "i"}, {"й", "y"}, {"к", "k"},
                    {"л", "l"}, {"м", "m"}, {"н", "n"}, {"о", "o"}, {"п", "p"}, {"р", "r"},
                    {"с", "s"}, {"т", "t"}, {"у", "u"}, {"ф", "f"}, {"х", "kh"}, {"ц", "ts"},
                    {"ч", "ch"}, {"ш", "sh"}, {"щ", "shch"}, {"ъ", ""}, {"ы", "y"}, {"ь", ""},
                    {"э", "e"}, {"ю", "yu"}, {"я", "ya"},
                    {"А", "A"}, {"Б", "B"}, {"В", "V"}, {"Г", "G"}, {"Д", "D"}, {"Е", "E"},
                    {"Ё", "Yo"}, {"Ж", "Zh"}, {"З", "Z"}, {"И", "I"}, {"Й", "Y"}, {"К", "K"},
                    {"Л", "L"}, {"М", "M"}, {"Н", "N"}, {"О", "O"}, {"П", "P"}, {"Р", "R"},
                    {"С", "S"}, {"Т", "T"}, {"У", "U"}, {"Ф", "F"}, {"Х", "Kh"}, {"Ц", "Ts"},
                    {"Ч", "Ch"}, {"Ш", "Sh"}, {"Щ", "Shch"}, {"Ъ", ""}, {"Ы", "Y"}, {"Ь", ""},
                    {"Э", "E"}, {"Ю", "Yu"}, {"Я", "Ya"},{" ",""}
                };

                var result = new StringBuilder();
                for (int i = 0; i < text.Length; i++)
                {
                    // Проверяем, есть ли текущий символ в словаре
                    if (translitDict.TryGetValue(text[i].ToString(), out string value))
                    {
                        result.Append(value);
                    }
                    else
                    {
                        // Если символа нет в словаре, оставляем как есть
                        result.Append(text[i]);
                    }
                }
                var rand = new Random();
               login = result + rand.Next(00, 99).ToString();
            } while (repo.GetLoginUser(login) != null);
            return login.ToString();
        }
    }
}
