using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

namespace zhdx.Utils
{
    public class ZRandom
    {
        public static string RandomString(int length)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var bit_count = (length * 6);
                var byte_count = ((bit_count + 7) / 8); // rounded up
                var bytes = new byte[byte_count];
                rng.GetBytes(bytes);
                return System.Convert.ToBase64String(bytes);
            }
        }
    }
}
