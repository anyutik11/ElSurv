using Serilog;
using System.Security.Cryptography;
using System.Text;

namespace web.Models;

public class Utils
{
    /// <summary>
    /// Вычисление SHA256
    /// </summary>
    /// <param name="data"></param>
    /// <param name="salt"></param>
    /// <returns></returns>
    public static string ComputePasswordSHA256(string data, string salt)
    {
        if (string.IsNullOrEmpty(data)) return "";
        try
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(data + salt));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++) builder.Append(bytes[i].ToString("x2"));
                return builder.ToString();
            }
        }
        catch (Exception ex)
        {
            Log.Error("Ошибка вычисления SHA256.", ex.Message);
        }
        return "";
    }
}
