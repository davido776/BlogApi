using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Models.settings
{
    public class JwtConfig
    {
        public TimeSpan TokenLifeTime { get; set; }

        public string SecretKey { get; set; }
    }
}
