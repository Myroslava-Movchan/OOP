﻿namespace Online_Store_Management.Services
{
    public class JwtOptions
    {
        public required string Key { get; set; }
        public required string Issuer { get; set; }
        public required string Audience { get; set; }
        public required double ExpiresInMinutes { get; set; }
    }
}
