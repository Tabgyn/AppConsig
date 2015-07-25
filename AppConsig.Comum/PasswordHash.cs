using System;
using System.Security.Cryptography;

namespace AppConsig.Comum
{
    /// <summary>
    /// Salted password hashing with PBKDF2-SHA1.
    /// Compatibility: .NET 3.0 and later.
    /// </summary>
    public class PasswordHash
    {
        // As seguintes constantes podem ser alteradas sem quebrar hashes existentes.
        public const int SALT_BYTE_SIZE = 24;
        public const int HASH_BYTE_SIZE = 24;
        public const int PBKDF2_ITERATIONS = 9999;
        public const int ITERATION_INDEX = 0;
        public const int SALT_INDEX = 1;
        public const int PBKDF2_INDEX = 2;

        /// <summary>
        /// Cria uma criptografia PBKDF2 com salt da senha.
        /// </summary>
        /// <param name="senha">A senha para criptografar.</param>
        /// <returns>A criptografia da senha.</returns>
        public static string CriarCriptografia(string senha)
        {
            // Gerar um salt aleatório.
            var csprng = new RNGCryptoServiceProvider();
            var salt = new byte[SALT_BYTE_SIZE];
            csprng.GetBytes(salt);

            // Criptografar a senha e codificar os parâmetros.
            var hash = PBKDF2(senha, salt, PBKDF2_ITERATIONS, HASH_BYTE_SIZE);

            return PBKDF2_ITERATIONS + ":" +
                Convert.ToBase64String(salt) + ":" +
                Convert.ToBase64String(hash);
        }

        /// <summary>
        /// Valida uma senha com sua criptografia correta.
        /// </summary>
        /// <param name="senha">A senha para validar.</param>
        /// <param name="criptografiaCorreta">a criptografia correta da senha.</param>
        /// <returns>True se a senha está correta. False caso contrário.</returns>
        public static bool ValidarSenha(string senha, string criptografiaCorreta)
        {
            // Extrair os parâmetros a partir da criptografia
            char[] delimiter = { ':' };
            var split = criptografiaCorreta.Split(delimiter);
            var iterations = Int32.Parse(split[ITERATION_INDEX]);
            var salt = Convert.FromBase64String(split[SALT_INDEX]);
            var hash = Convert.FromBase64String(split[PBKDF2_INDEX]);
            var testHash = PBKDF2(senha, salt, iterations, hash.Length);

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
        private static byte[] PBKDF2(string password, byte[] salt, int iterations, int outputBytes)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt) { IterationCount = iterations };

            return pbkdf2.GetBytes(outputBytes);
        }
    }
}
