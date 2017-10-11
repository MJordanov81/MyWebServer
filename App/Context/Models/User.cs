namespace App.Context.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Username { get; set; }

        [Required]
        [MaxLength(50)]
        public string Password { get; set; }

        public DateTime RegistrationDate { get; set; }

        public IList<Order> Orders { get; set; } = new List<Order>();
    }
}
