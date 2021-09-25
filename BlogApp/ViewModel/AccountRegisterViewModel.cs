using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.ViewModel
{
    public class AccountRegisterViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string Firstname { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string Lastname { get; set; }
        [Required]
        [StringLength(int.MaxValue, ErrorMessage = "Username must be greater than 4 character.", MinimumLength = 4)]
        public string Username { get; set; }
        [Required]
        [StringLength(int.MaxValue, ErrorMessage = "password must be greater than 4 character.", MinimumLength = 4)]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Confirm Password")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

    }
}
