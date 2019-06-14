using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using CambiaUnaVida.Models.Dominio;

namespace CambiaUnaVida.Models.Repository.EntityRepository
{
    public class CitaAdopcionRepository : IGeneric<CitaAdopcion>
    {
        ApplicationDbContext db;

        public CitaAdopcionRepository(ApplicationDbContext context)
        {
            db = context;
        }
        public void actualizar(CitaAdopcion Tentity)
        {
            CitaAdopcion TentityAux = obtenerPorId(Tentity);

            if (TentityAux != null)
            {
                db.Entry<CitaAdopcion>(TentityAux).State = EntityState.Detached;
                db.Entry<CitaAdopcion>(TentityAux).State = EntityState.Modified;
            }
        }

        public void crear(CitaAdopcion Tentity)
        {
            db.CitasAdopciones.Add(Tentity);
        }

        public void eliminar(CitaAdopcion Tentity)
        {
            db.Entry<CitaAdopcion>(obtenerPorId(Tentity)).State = EntityState.Deleted;
        }

        public IQueryable<CitaAdopcion> encontrarPor(Expression<Func<CitaAdopcion, bool>> condicion)
        {
            return db.CitasAdopciones.Where(condicion).Select(c => c);
        }

        public CitaAdopcion obtenerPorId(CitaAdopcion Tentity)
        {
            return db.CitasAdopciones.FirstOrDefault(c => c.id == Tentity.id);
        }

        public IQueryable<CitaAdopcion> obtenerTodo()
        {
            return db.CitasAdopciones.Select(c => c);
        }
    }
}