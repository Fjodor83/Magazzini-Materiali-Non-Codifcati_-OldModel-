﻿
using MagazziniMaterialiAPI.Models.Entity;
using Microsoft.AspNetCore.Identity;

namespace MagazziniGessi.Api.Models
{
    public class RegisterModel
    {

        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public Ruolo Ruolo { get; set; }

    }
}