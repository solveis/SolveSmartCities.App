using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolveChicago.Entities;

namespace SolveChicago.App.Common.Entities
{
    public class BaseEntity
    {
        protected SolveChicagoEntities db;

        public BaseEntity(SolveChicagoEntities db)
        {
            this.db = db;
        }

        public BaseEntity()
        {
            this.db = new SolveChicagoEntities();
        }
    }
}
