using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace GuestHouseBookingApplication_Server.Security
{
    public class PasswordHasher
    {
        public static (string hash, string salt) HashPassword(string password)
        {
            // generate a 128-bit salt using a secure PRNG
            byte[] saltBytes = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }
            string salt = Convert.ToBase64String(saltBytes);

            // derive a 256-bit subkey (use HMACSHA256) with 100,000 iterations (adjust as needed)
            string hashed = Convert.ToBase64String(
                KeyDerivation.Pbkdf2(
                    password: password!,
                    salt: saltBytes,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 100_000,
                    numBytesRequested: 256 / 8
                )
            );

            return (hashed, salt);
        }

        public static bool Verify(string password, string storedHash, string storedSalt)
        {
            byte[] saltBytes = Convert.FromBase64String(storedSalt);
            string hashed = Convert.ToBase64String(
                KeyDerivation.Pbkdf2(
                    password: password!,
                    salt: saltBytes,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 100_000,
                    numBytesRequested: 256 / 8
                )
            );

            return hashed == storedHash;
        }
    }
}
