﻿using System.ComponentModel.DataAnnotations;

namespace BookStore.Data.Models
{
    public class RegisterModel
    { 
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
