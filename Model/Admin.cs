﻿using System.ComponentModel.DataAnnotations;

namespace MVVMauiApp.Model
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string contrasenia { get; set; } = "1q2w3e4r";
    }
}
