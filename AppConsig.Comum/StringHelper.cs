using System;
using System.Security.Cryptography;

namespace AppConsig.Common
{
    public class StringHelper
    {
        public static string GetRandomText(int lenght = 10)
        {
            string randomString;

            using (var randomNumberGenerator = new RNGCryptoServiceProvider())
            {
                var data = new byte[64];
                randomNumberGenerator.GetBytes(data);

                randomString = Convert.ToBase64String(data);
            }

            randomString = randomString.Substring(0, lenght);

            return randomString;
        }
    }
}