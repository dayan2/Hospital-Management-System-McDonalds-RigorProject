#region Using Directives
using Mcd.HospitalManagementSystem.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
#endregion

namespace Mcd.HospitalManagement.Web.Models
{
    public class UserRoleModel
    {
        [Required(ErrorMessage = "Id is Required")]
        public int Id { get; set; }

        [Required(ErrorMessage="Role is Required")]
        [Display(Name = "User Role")]
        public string Role { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}