using SolveChicago.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveChicago.Service
{
    public class CommunicationService : BaseService
    {
        public CommunicationService(SolveChicagoEntities db) : base(db) { }

        public void Log(DateTime date, int organizationId, string communicationType, string userId, bool success)
        {
            db.Communications.Add(new Communication
            {
                Date = date,
                OrganizationId = organizationId,
                Type = communicationType,
                UserId = userId,
                Success = success
            });
            db.SaveChanges();
        }
    }
}
