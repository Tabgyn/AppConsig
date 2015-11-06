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

        public static string MascaraCnpjCpf(string pCnpjCpf)
        {
            var result = "";

            if (pCnpjCpf.Length == 14)
            {
                result = pCnpjCpf.Insert(2, ".").Insert(6, ".").Insert(10, "/").Insert(15, "-");
            }

            if (pCnpjCpf.Length == 11)
            {
                result = pCnpjCpf.Insert(3, ".").Insert(7, ".").Insert(11, "-");
            }

            if ((pCnpjCpf.Length != 11) && (pCnpjCpf.Length != 14))
            {
                result = pCnpjCpf;
            }

            return result;
        }
    }
}