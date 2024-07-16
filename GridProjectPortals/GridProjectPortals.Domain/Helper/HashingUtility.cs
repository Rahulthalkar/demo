using System.Security.Cryptography;

namespace AkkonMassive.Domain.Helper
{
    public class HashingUtility
    {
        private const int SaltBitSize = 64;
        private const int Iterations = 10000;
        private const int HashBitSize = 256;

        public static string HashString(string valueToHash, string salt)
        {
            byte[] hash;
            using (var generator = new Rfc2898DeriveBytes(valueToHash, Convert.FromBase64String(salt), Iterations, HashAlgorithmName.SHA1))
            {
                hash = generator.GetBytes(HashBitSize / 8);
            }
            return Convert.ToBase64String(hash);
        }

        public static string GenerateSalt()
        {
            var rng = RandomNumberGenerator.Create();
            var salt = new byte[SaltBitSize / 8];
            rng.GetBytes(salt);
            return Convert.ToBase64String(salt);
        }
    }
}
