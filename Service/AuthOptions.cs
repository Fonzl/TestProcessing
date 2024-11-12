using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Service
{
    public class AuthOptions
    {
        public const string ISSUER = "MyAuthServer"; // издатель токена
        public const string AUDIENCE = "MyAuthClient"; // потребитель токена
        const string KEY = "$co9H$JoYuA8)*7k9zD^C0DTwS*A\\&v.f?R~RL3A,T(E)KF.iS";   // ключ для шифрации
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
