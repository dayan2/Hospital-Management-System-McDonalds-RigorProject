#region Using Directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion



namespace Mcd.HospitaManagementSystem.Business
{
    public interface IDoctorRecommendationManager
    {
        IEnumerable<DoctorRecommendationDTO> GetDoctorRecommendationDetails();
        bool SaveTransferDoctorDetails(DoctorRecommendationDTO doctor);
        DoctorRecommendationDTO GetDoctorRecommendationById(int id);
        bool RemoveDoctorRecommendationDetails(int id);
    }
}
