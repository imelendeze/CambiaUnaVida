using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using CambiaUnaVida.Models;

namespace CambiaUnaVida.Models.Repository.EntityRepository
{
    public class ApplicationUserRepository : IGeneric<ApplicationUser>
    {
        ApplicationDbContext db;

        public ApplicationUserRepository(ApplicationDbContext context)
        {
            db = context;
        }
        public void actualizar(ApplicationUser Tentity)
        {
            ApplicationUser usuAux = obtenerPorId(Tentity);

            if (usuAux != null)
            {
                db.Entry(usuAux).State = EntityState.Detached;
                db.Entry(usuAux).State = EntityState.Modified;
            }
        }

        public void crear(ApplicationUser Tentity)
        {
            db.Users.Add(Tentity);
        }

        public void eliminar(ApplicationUser Tentity)
        {
            db.Entry<ApplicationUser>(obtenerPorId(Tentity)).State = EntityState.Deleted;
        }

        public IQueryable<ApplicationUser> encontrarPor(Expression<Func<ApplicationUser, bool>> condicion)
        {
            return db.Users.Where(condicion).Select(c => c);
        }

        public ApplicationUser obtenerPorId(ApplicationUser Tentity)
        {
            return db.Users.FirstOrDefault(c => c.Id == Tentity.Id);
        }

        public IQueryable<ApplicationUser> obtenerTodo()
        {
            return db.Users.Select(c => c);
        }
    }
}