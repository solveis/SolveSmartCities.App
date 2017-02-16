using SolveChicago.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveChicago.App.Service
{
    public class BaseService : IDisposable
    {
        protected SolveChicagoEntities db = new SolveChicagoEntities();

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
