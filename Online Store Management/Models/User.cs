﻿namespace Online_Store_Management.Models
{
    public class User
    {
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
    }
}
