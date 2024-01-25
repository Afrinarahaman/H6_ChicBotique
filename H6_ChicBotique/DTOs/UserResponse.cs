﻿using H6_ChicBotique.Helpers;

namespace H6_ChicBotique.DTOs
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Role Role { get; set; }
        public AccountInfoResponse Account { get; set; }
        public HomeAddressResponse HomeAddress { get; set; }
    }
}
