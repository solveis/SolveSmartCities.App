using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveChicago.Common.Models.Profile.Member
{
    public class MemberProfileSchools
    {
        public int MemberId { get; set; }
        public SchoolEntity[] Schools { get; set; }
    }

    public class SchoolEntity
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public bool IsCurrent { get; set; }
        public string Degree { get; set; }

    }
}
