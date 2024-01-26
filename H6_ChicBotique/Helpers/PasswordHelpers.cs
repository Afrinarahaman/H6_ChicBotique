using System.Security.Cryptography;
using System.Text;

namespace H6_ChicBotique.Helpers
{
    public class PasswordHelpers
    {
        public static string HashPassword(string password)
        {
            SHA256 hash = SHA256.Create();

            var passwordBytes = Encoding.Default.GetBytes(password);

            var hashedpassword = hash.ComputeHash(passwordBytes);

            return Convert.ToHexString(hashedpassword);

        }
        // Helper method to verify a password
        public static bool VerifyPassword(string password, string salt, string expectedHash)
        {
            string actualHash = HashPassword($"{password}{salt}");
            return actualHash == expectedHash;
        }
      
    }
}
