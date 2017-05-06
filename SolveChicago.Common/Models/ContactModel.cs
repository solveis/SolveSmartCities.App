using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SolveChicago.Common.Models
{
    public class ContactModel
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}