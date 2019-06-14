using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using CambiaUnaVida.Models.Dominio;

namespace CambiaUnaVida.Models.Repository.EntityRepository
{
    public class PeticionAdopcionRepository : IGeneric<PeticionAdopcion>
    {
        ApplicationDbContext db;

        public PeticionAdopcionRepository(ApplicationDbContext context)
        {
            db = context;
        }
        public void actualizar(PeticionAdopcion Tentity)
        {
            PeticionAdopcion TentityAux = obtenerPorId(Tentity);

            if (TentityAux != null)
            {
                db.Entry<PeticionAdopcion>(TentityAux).State = EntityState.Detached;
                db.Entry<PeticionAdopcion>(TentityAux).State = EntityState.Modified;
            }
        }

        public void crear(PeticionAdopcion Tentity)
        {
            db.PeticionesAdopcion.Add(Tentity);
        }

        public void eliminar(PeticionAdopcion Tentity)
        {
            db.Entry<PeticionAdopcion>(obtenerPorId(Tentity)).State = EntityState.Deleted;
        }

        public IQueryable<PeticionAdopcion> encontrarPor(Expression<Func<PeticionAdopcion, bool>> condicion)
        {
            return db.PeticionesAdopcion.Where(condicion).Select(c => c);
        }

        public PeticionAdopcion obtenerPorId(PeticionAdopcion Tentity)
        {
            return db.PeticionesAdopcion.FirstOrDefault(c => c.id == Tentity.id);
        }

        public IQueryable<PeticionAdopcion> obtenerTodo()
        {
            return db.PeticionesAdopcion.Select(c => c);
        }
    }
}