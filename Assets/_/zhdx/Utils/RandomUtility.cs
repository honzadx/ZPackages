using System.Security.Cryptography;

namespace zhdx
{
    namespace Utils
    {
        public class RandomUtility
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
}
