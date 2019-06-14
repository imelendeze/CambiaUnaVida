using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using CambiaUnaVida.Models.Dominio;

namespace CambiaUnaVida.Models.Repository.EntityRepository
{
    public class GatoRepository : IGeneric<Gato>
    {
        private ApplicationDbContext db;

        public GatoRepository(ApplicationDbContext context)
        {
            db = context;
        }
        public void actualizar(Gato Tentity)
        {
            Gato TentityAux = obtenerPorId(Tentity);

            if (TentityAux != null)
            {
                db.Entry<Gato>(TentityAux).State = EntityState.Detached;
                db.Entry<Gato>(TentityAux).State = EntityState.Modified;
            }
        }

        public void crear(Gato Tentity)
        {
            db.Gatos.Add(Tentity);
        }

        public void eliminar(Gato Tentity)
        {
            db.Entry<Gato>(obtenerPorId(Tentity)).State = EntityState.Deleted;
        }

        public IQueryable<Gato> encontrarPor(Expression<Func<Gato, bool>> condicion)
        {
            return db.Gatos.Where(condicion).Select(c => c);
        }

        public Gato obtenerPorId(Gato Tentity)
        {
            return db.Gatos.FirstOrDefault(c => c.id == Tentity.id);
        }

        public IQueryable<Gato> obtenerTodo()
        {
            return db.Gatos.Select(c => c);
        }
    }
}