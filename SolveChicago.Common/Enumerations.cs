using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveChicago.Common
{
    public class Enumerations
    {
        public enum Role
        {
            Member,
            CaseManager,
            Corporation,
            Nonprofit,
            Admin,
            Referrer
        }

        public enum FamilyRelationshipTypes
        {
            Spouse,
            ParentChild,
            Cousin,
            UncleNephew,
            Sibling
        }

        public enum FamilyRelationshipRoles
        {
            Husband,
            Wife,
            Child,
            Father,
            Mother,
            Uncle, 
            Aunt,
            Brother,
            Sister,
            Cousin
        }
    }
}
