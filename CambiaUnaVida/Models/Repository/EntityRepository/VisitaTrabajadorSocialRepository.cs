using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using CambiaUnaVida.Models.Dominio;

namespace CambiaUnaVida.Models.Repository.EntityRepository
{
    public class VisitaTrabajadorSocialRepository : IGeneric<VisitaTrabajadorSocial>
    {
        ApplicationDbContext db;

        public VisitaTrabajadorSocialRepository(ApplicationDbContext context)
        {
            db = context;
        }
        public void actualizar(VisitaTrabajadorSocial Tentity)
        {
            VisitaTrabajadorSocial TentityAux = obtenerPorId(Tentity);

            if (TentityAux != null)
            {
                db.Entry<VisitaTrabajadorSocial>(TentityAux).State = EntityState.Detached;
                db.Entry<VisitaTrabajadorSocial>(TentityAux).State = EntityState.Modified;
            }
        }

        public void crear(VisitaTrabajadorSocial Tentity)
        {
            db.VisitasTrabajadorSocial.Add(Tentity);
        }

        public void eliminar(VisitaTrabajadorSocial Tentity)
        {
            db.Entry<VisitaTrabajadorSocial>(obtenerPorId(Tentity)).State = EntityState.Deleted;
        }

        public IQueryable<VisitaTrabajadorSocial> encontrarPor(Expression<Func<VisitaTrabajadorSocial, bool>> condicion)
        {
            return db.VisitasTrabajadorSocial.Where(condicion).Select(c => c);
        }

        public VisitaTrabajadorSocial obtenerPorId(VisitaTrabajadorSocial Tentity)
        {
            return db.VisitasTrabajadorSocial.FirstOrDefault(c => c.id == Tentity.id);
        }

        public IQueryable<VisitaTrabajadorSocial> obtenerTodo()
        {
            return db.VisitasTrabajadorSocial.Select(c => c);
        }
    }
}