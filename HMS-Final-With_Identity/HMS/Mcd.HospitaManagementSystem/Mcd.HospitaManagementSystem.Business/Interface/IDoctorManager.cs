#region Using Directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion


namespace Mcd.HospitaManagementSystem.Business
{
    public interface IDoctorManager
    {
        IEnumerable<DoctorRoleDTO> GetDoctors();
        DoctorRoleDTO GetDoctorsById(int id);
        //get Patient details_Appointments
        bool AddDoctor(DoctorRoleDTO doctor);
        bool RemoveDoctor(int id);
        bool UpdateDoctor(DoctorRoleDTO doctor);
        bool TransferDoctor(DoctorRecommendationDTO doctor);
        IEnumerable<DoctorSpecialityDTO> GetAllDoctorSpecialityTypes();
        DoctorDetailDTO GetDoctorByUserIdForProfileDetail(int id);
        string DoctorAlreadyExist(string name);

    }
}
