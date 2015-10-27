using System;
using System.Security.Cryptography;

namespace AppConsig.Common
{
    /// <summary>
    /// Salted value hashing with PBKDF2-SHA1.
    /// Compatibility: .NET 3.0 and later.
    /// </summary>
    public class HashHelper
    {
        // The following constants may be changed without breaking existing hashes.
        public const int SaltByteSize = 24;
        public const int HashByteSize = 24;
        public const int Pbkdf2Iterations = 9999;
        public const int IterationIndex = 0;
        public const int SaltIndex = 1;
        public const int Pbkdf2Index = 2;

        /// <summary>
        /// Creates a salted PBKDF2 hash of the value.
        /// </summary>
        /// <param name="value">The value to hash.</param>
        /// <returns>The hash of the password.</returns>
        public static string CreateHash(string value)
        {
            // Generate a random salt
            var csprng = new RNGCryptoServiceProvider();
            var salt = new byte[SaltByteSize];
            csprng.GetBytes(salt);

            // Hash the value and encode the parameters
            var hash = Pbkdf2(value, salt, Pbkdf2Iterations, HashByteSize);

            return Pbkdf2Iterations + ":" +
                   Convert.ToBase64String(salt) + ":" +
                   Convert.ToBase64String(hash);
        }

        /// <summary>
        /// Validates a value given a hash of the correct one.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="correctHash">A hash of the correct value.</param>
        /// <returns>True if the value is correct. False otherwise.</returns>
        public static bool ValidateHash(string value, string correctHash)
        {
            // Extract the parameters from the hash
            char[] delimiter = { ':' };
            var split = correctHash.Split(delimiter);
            var iterations = int.Parse(split[IterationIndex]);
            var salt = Convert.FromBase64String(split[SaltIndex]);
            var hash = Convert.FromBase64String(split[Pbkdf2Index]);
            var testHash = Pbkdf2(value, salt, iterations, hash.Length);

            return SlowEquals(hash, testHash);
        }

        /// <summary>
        /// Compares two byte arrays in length-constant time. This comparison
        /// method is used so that value hashes cannot be extracted from
        /// on-line systems using a timing attack and then attacked off-line.
        /// </summary>
        /// <param name="a">The first byte array.</param>
        /// <param name="b">The second byte array.</param>
        /// <returns>True if both byte arrays are equal. False otherwise.</returns>
        private static bool SlowEquals(byte[] a, byte[] b)
        {
            var diff = (uint)a.Length ^ (uint)b.Length;
            for (var i = 0; i < a.Length && i < b.Length; i++)
                diff |= (uint)(a[i] ^ b[i]);

            return diff == 0;
        }

        /// <summary>
        /// Computes the PBKDF2-SHA1 hash of a value.
        /// </summary>
        /// <param name="value">The value to hash.</param>
        /// <param name="salt">The salt.</param>
        /// <param name="iterations">The PBKDF2 iteration count.</param>
        /// <param name="outputBytes">The length of the hash to generate, in bytes.</param>
        /// <returns>A hash of the value.</returns>
        private static byte[] Pbkdf2(string value, byte[] salt, int iterations, int outputBytes)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(value, salt) { IterationCount = iterations };

            return pbkdf2.GetBytes(outputBytes);
        }
    }
}
