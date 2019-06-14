using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CambiaUnaVida.Models.Repository.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext Context; //Se hace esto para que no se generen los Dbsets

        public UnitOfWork(ApplicationDbContext context)
        {
            Context = context;
        }

        public void commit()
        {
            Context.SaveChanges();
        }

        public void rollback() //Deshace los cambios
        {
            foreach (var entry in Context.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.State = EntityState.Modified;
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                }
            }
        }
    }
}