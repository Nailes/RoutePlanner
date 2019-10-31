using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace RoutePlanner
{
    public class AuthOptions
    {
        public const string ISSUER = "RoutePlannerServer";
        public const string AUDIENCE = "http://localhost:9998/";
        const string KEY = "pRl1d%*B09tx0#9)tO#l%&3j07l4(e3K";
        public const int LIFETIME = 2880;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
