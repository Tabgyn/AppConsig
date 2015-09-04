using System;
using System.Security.Cryptography;
using static System.Int32;

namespace AppConsig.Comum
{
    /// <summary>
    /// Salted password hashing with PBKDF2-SHA1.
    /// Compatibility: .NET 3.0 and later.
    /// </summary>
    public class HashHelper
    {
        // As seguintes constantes podem ser alteradas sem quebrar hashes existentes.
        public const int SaltByteSize = 24;
        public const int HashByteSize = 24;
        public const int Pbkdf2Iterations = 9999;
        public const int IterationIndex = 0;
        public const int SaltIndex = 1;
        public const int Pbkdf2Index = 2;

        /// <summary>
        /// Cria uma criptografia PBKDF2 com salt do valor.
        /// </summary>
        /// <param name="valor">A senha para criptografar.</param>
        /// <returns>A criptografia da senha.</returns>
        public static string HashPbkdf2(string valor)
        {
            // Gerar um salt aleatório.
            var csprng = new RNGCryptoServiceProvider();
            var salt = new byte[SaltByteSize];
            csprng.GetBytes(salt);

            // Criptografar a senha e codificar os parâmetros.
            var hash = Pbkdf2(valor, salt, Pbkdf2Iterations, HashByteSize);

            return Pbkdf2Iterations + ":" +
                Convert.ToBase64String(salt) + ":" +
                Convert.ToBase64String(hash);
        }

        /// <summary>
        /// Valida um valor com sua criptografia correta.
        /// </summary>
        /// <param name="valor">O valor para validar.</param>
        /// <param name="criptografiaCorreta">a criptografia correta da senha.</param>
        /// <returns>True se o valor está correto. False caso contrário.</returns>
        public static bool ValidarHash(string valor, string criptografiaCorreta)
        {
            // Extrair os parâmetros a partir da criptografia
            char[] delimiter = { ':' };
            var split = criptografiaCorreta.Split(delimiter);
            var iterations = Parse(split[IterationIndex]);
            var salt = Convert.FromBase64String(split[SaltIndex]);
            var hash = Convert.FromBase64String(split[Pbkdf2Index]);
            var testHash = Pbkdf2(valor, salt, iterations, hash.Length);

            return SlowEquals(hash, testHash);
        }

        /// <summary>
        /// Compares two byte arrays in length-constant time. This comparison
        /// method is used so that password hashes cannot be extracted from
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
        /// Computes the PBKDF2-SHA1 hash of a password.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <param name="salt">The salt.</param>
        /// <param name="iterations">The PBKDF2 iteration count.</param>
        /// <param name="outputBytes">The length of the hash to generate, in bytes.</param>
        /// <returns>A hash of the password.</returns>
        private static byte[] Pbkdf2(string password, byte[] salt, int iterations, int outputBytes)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt) { IterationCount = iterations };

            return pbkdf2.GetBytes(outputBytes);
        }
    }
}
