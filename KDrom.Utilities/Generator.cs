using System.Security.Cryptography;

namespace KDrom.Utilities
{
    public static class Generator
    {
        public static string GenerateVerificationCode(int codeLength)
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                int randomCode = 0;
                byte[] randomNumber = new byte[4];

                int minValue = (int)Math.Pow(10, codeLength - 1);
                int maxValue = (int)Math.Pow(10, codeLength);

                while (randomCode < minValue || randomCode >= maxValue)
                {
                    rng.GetBytes(randomNumber);
                    randomCode = BitConverter.ToInt32(randomNumber, 0);
                }

                return randomCode.ToString().PadLeft(codeLength, '0');
            }
        }

        public static string GenerateRandomString(int length)
        {
            var chars = new char[length];
            var randomBytes = new byte[length];

            using (var rnd = RandomNumberGenerator.Create())
            {
                rnd.GetBytes(randomBytes);
            }

            for (int i = 0; i < length; i++)
            {
                int randomValue = randomBytes[i] % 62;

                chars[i] = randomValue switch
                {
                    < 10 => (char)('0' + randomValue), // 0-9
                    < 36 => (char)('A' + (randomValue - 10)), //A-Z
                    _    => (char)('a' + (randomValue - 36)) //a-z
                };
            }

            return new string(chars);
        }
    }
}
