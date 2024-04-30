using System.Security.Cryptography;
using System.Text;

namespace CentralDeUsuarios.Infra.Data.Helpers;

public static class MD5Helper
{
    public static string Encrypt(string value)
    {
        using (var md5 = MD5.Create())
        {
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(value));

            var result = string.Empty;
            foreach (var item in hash)
            {
                result += item.ToString("X2");
            }

            return result;
        }
    }
}
