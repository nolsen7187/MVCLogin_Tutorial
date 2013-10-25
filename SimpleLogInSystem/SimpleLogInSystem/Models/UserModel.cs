using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleLogInSystem.Models
{
    public class UserModel
    {
        [Required]
        [EmailAddress]
        [StringLength(150)]
        [Display(Name="Email address: ")]
        public string email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength=6)]
        [Display(Name="Password")]
        public string password { get; set; }
    }
}