using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolveChicago.Common.Models.Profile.Member;

namespace SolveChicago.Common.Interfaces.Profile.Member
{

    public interface IMemberProfileJobs
    {
        int MemberId { get; set; }
        bool? CurrentlyLooking { get; set; }
        JobEntity[] Jobs { get; set; }
    }
}
