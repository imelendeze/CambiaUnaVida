using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using CambiaUnaVida.Models.Dominio;

namespace CambiaUnaVida.Models.Repository.EntityRepository
{
    public class AdopcionRepository : IGeneric<Adopcion>
    {
        ApplicationDbContext db;

        public AdopcionRepository(ApplicationDbContext context)
        {
            db = context;
        }
        public void actualizar(Adopcion Tentity)
        {
            Adopcion TentityAux = obtenerPorId(Tentity);

            if (TentityAux != null)
            {
                db.Entry<Adopcion>(TentityAux).State = EntityState.Detached;
                db.Entry<Adopcion>(TentityAux).State = EntityState.Modified;
            }
        }

        public void crear(Adopcion Tentity)
        {
            db.Adopciones.Add(Tentity);
        }

        public void eliminar(Adopcion Tentity)
        {
            db.Entry<Adopcion>(obtenerPorId(Tentity)).State = EntityState.Deleted;
        }

        public IQueryable<Adopcion> encontrarPor(Expression<Func<Adopcion, bool>> condicion)
        {
            return db.Adopciones.Where(condicion).Select(c => c);
        }

        public Adopcion obtenerPorId(Adopcion Tentity)
        {
            return db.Adopciones.FirstOrDefault(c => c.id == Tentity.id);
        }

        public IQueryable<Adopcion> obtenerTodo()
        {
            return db.Adopciones.Select(c => c);
        }
    }
}