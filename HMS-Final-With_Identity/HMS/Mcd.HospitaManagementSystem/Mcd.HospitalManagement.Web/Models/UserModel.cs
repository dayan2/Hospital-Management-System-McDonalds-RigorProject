using Mcd.HospitalManagementSystem.Data;
using System;
using System.Collections.Generic;
#region Using Directives
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
#endregion

namespace Mcd.HospitalManagement.Web.Models
{
    public class UserModel
    {
        [Required(ErrorMessage = "Id is Required")]
        public int Id { get; set; }

        [Display(Name = "User Name")]
        [Required(ErrorMessage = "Username is Required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name="User Role")]
        public string UserRoleType { get; set; }

        public int UserRoleId { get; set; }
        public UserRole UserRole { get; set; }

        public IEnumerable<UserRoleModel> UserRoles { get; set; } 
    }
}