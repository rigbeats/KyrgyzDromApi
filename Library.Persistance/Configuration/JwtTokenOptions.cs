using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDrom.Persistance.Configuration
{
    public class JwtTokenOptions
    {
        public string Issuer { get; set; }

        public string Audience { get; set; }

        public string Secret { get; set; }

        public int AccessTokenExpiryMinutes { get; set; }

        public int RefreshTokenExpiryMinutes { get; set; }
    }
}
