using System;
using System.Security.Cryptography;

namespace AppConsig.Comum
{
    public class StringHelper
    {
        public static string GetRandomString(int length)
        {
            if (length < 0)
            {
                throw new InvalidOperationException("length can not be less than 10");
            }

            string value;

            using (var randomNumberGenerator = new RNGCryptoServiceProvider())
            {
                var randomNumber = new byte[64];
                randomNumberGenerator.GetBytes(randomNumber);

                value = Convert.ToBase64String(randomNumber);
            }

            value = value.Substring(0, length);

            return value;
        }
    }
}