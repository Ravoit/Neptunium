using System;
using System.Security.Cryptography;
using System.Text;

namespace Neptunium.Util
{
    public static class Security
    {
        private static readonly int[] ReplaceOrder =
        {
            35, 6, 4, 25, 7, 8, 36, 16, 20, 37, 12, 31, 39, 38, 21, 5, 33, 15, 9, 13, 29, 23, 32, 22, 2, 27, 1, 10, 30,
            24, 0, 19, 26, 14, 18, 34, 17, 28, 11, 3
        };

        private static readonly int[] NewOrder =
        {
            30, 26, 24, 39, 2, 15, 1, 4, 5, 18, 27, 38, 10, 19, 33, 17, 7, 36, 34, 31, 8, 14, 23, 21, 29, 3, 32, 25, 37,
            20, 28, 11, 22, 16, 35, 0, 6, 9, 13, 12
        };

        private static char[] GetSHA1(string plaintext)
        {
            using var sha1 = SHA1.Create();

            var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(plaintext));
            return BitConverter.ToString(hash).Replace("-", "").ToUpper().ToCharArray();
        }

        public static string EncodePassword(string psw, string key)
        {
            if (string.IsNullOrEmpty(psw) || string.IsNullOrEmpty(key)) return string.Empty;

            var p1 = psw.Substring(0, 1) + key.Substring(0, 10) + psw.Substring(1) + key.Substring(10);

            p1 = p1.Replace(" ", "");

            var p2 = GetSHA1(p1);

            var result = new char[p2.Length];

            for (var i = 0; i < result.Length; i++)
            {
                result[i] = p2[NewOrder[i]];
                //result[ReplaceOrder[i]] = p2[i];
            }

            return new string(result);
        }
    }
}