using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SolveChicago.Web.Models.Admin
{
    public class AdminInviteModel
    {
        public string EmailToInvite { get; set; }
        public int AdminId { get; set; }
    }
}