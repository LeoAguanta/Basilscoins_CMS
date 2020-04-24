using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InSys.ITI.Models.Models
{

    [NotMapped]
    public class vKioskMRFStatus
    {
        public int ID { get; set; }
        public string MRFNumber { get; set; }
        public string Designation { get; set; }
        public DateTime? PostingDate { get; set; }
        public int? RequestHeadCount { get; set; }
        public int? ShortList { get; set; }
        public int? UnProcessed { get; set; }
        public int? OnProcess { get; set; }
        public int? Hired { get; set; }
        public int? Others { get; set; }
        public int? TotalCount { get; set; }
        public int ID_Company { get; set; }
        public DateTime? ApprovedDate { get; set; }
        [NotMapped]
        public string TimeSpanSinceApproved
        {
            get
            {

                if (ApprovedDate != null)
                {
                    DateTime _ApprovedDate = Convert.ToDateTime(this.ApprovedDate);
                    DateTime _CurrentDate = DateTime.Now;

                    return Convert.ToInt32((_CurrentDate - _ApprovedDate).TotalDays).ToString();

                }
                else
                {
                    return "";
                }

            }
        }
    }

    public class vKioskMRFStatusDetails
    {
        public int ID { get; set; }
        public string RefNum { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public int? ID_RecruitmentStatus { get; set; }
        public int? c_ID_RecruitmentStatus { get; set; }
        public string ApplicationStatus { get; set; }
    }

    public class vKioskMRFStatusDetailCount
    {
        public string ApplicationStatus { get; set; }
        public int TotalCount { get; set; }
    }

}
