using System;
using System.Collections.Generic;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enum;

namespace HospitalLibrary.DTOs
{
    public class RegistrationDto
    {
        public string Password { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string> Interests { get; set; }
    }
}