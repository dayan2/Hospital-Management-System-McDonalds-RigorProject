using Mcd.HospitalManagementSystem.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mcd.HospitaManagementSystem.Business
{
    public class UserDTO
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public int UserRoleId { get; set; }

        public UserRole UserRole { get; set; }

        public string UserRoleType { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
