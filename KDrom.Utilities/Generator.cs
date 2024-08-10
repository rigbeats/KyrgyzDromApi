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
    }
}
