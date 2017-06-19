using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using static SolveChicago.Common.Helpers;

namespace SolveChicago.Common.Models
{
    public class UploadModel
    {
        public int NonprofitId { get; set; }
        public HttpPostedFileBase CsvFile { get; set; }
    }

    public class ClientList
    {
        public string SSN { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public bool? IsHeadOfHousehold { get; set; }
        public decimal? Income { get; set; }
        public bool? IsMilitary { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string Skills { get; set; }
        public string Interests { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
