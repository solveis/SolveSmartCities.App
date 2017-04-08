using SolveChicago.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveChicago.Service
{
    public class GovernmentProgramService : BaseService
    {
        public GovernmentProgramService(SolveChicagoEntities db) : base(db) { }

        public GovernmentProgram [] Get()
        {
            return db.GovernmentPrograms.ToArray();
        }
    }
}
