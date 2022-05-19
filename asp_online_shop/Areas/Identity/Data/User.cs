using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using asp_online_shop.Models;
using Microsoft.AspNetCore.Identity;

namespace asp_online_shop.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the User class

    public class User : IdentityUser
    {
        [PersonalData]
        [Column(TypeName= "nvarchar(200)")]
        [Required]
        public string Firstname { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(200)")]
        [Required]
        public string Surname { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(200)")]
        public string Patronymic { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(1)")]
        public string Gender { get; set; }

        [PersonalData]
        [Column(TypeName = "date")]
        public DateTime? Dob { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(300)")]
        public string Address { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(50)")]
        [Required]
        public string Phone { get; set; }

        public List<Order> Orders { get; set; } = new List<Order>();

    }
}
