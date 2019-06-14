using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CambiaUnaVida.Models.Repository.Infrastructure
{
    public interface IUnitOfWork
    {
        void commit();
        void rollback();
    }
}
