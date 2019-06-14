using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using CambiaUnaVida.Models.Dominio;

namespace CambiaUnaVida.Models.Repository.EntityRepository
{
    public class CitaVeterinarioRepository : IGeneric<CitaVeterinario>
    {
        ApplicationDbContext db;

        public CitaVeterinarioRepository(ApplicationDbContext context)
        {
            db = context;
        }
        public void actualizar(CitaVeterinario Tentity)
        {
            CitaVeterinario TentityAux = obtenerPorId(Tentity);

            if (TentityAux != null)
            {
                db.Entry<CitaVeterinario>(TentityAux).State = EntityState.Detached;
                db.Entry<CitaVeterinario>(TentityAux).State = EntityState.Modified;
            }
        }

        public void crear(CitaVeterinario Tentity)
        {
            db.CitasVeterinarios.Add(Tentity);
        }

        public void eliminar(CitaVeterinario Tentity)
        {
            db.Entry<CitaVeterinario>(obtenerPorId(Tentity)).State = EntityState.Deleted;
        }

        public IQueryable<CitaVeterinario> encontrarPor(Expression<Func<CitaVeterinario, bool>> condicion)
        {
            return db.CitasVeterinarios.Where(condicion).Select(c => c);
        }

        public CitaVeterinario obtenerPorId(CitaVeterinario Tentity)
        {
            return db.CitasVeterinarios.FirstOrDefault(c => c.id == Tentity.id);
        }

        public IQueryable<CitaVeterinario> obtenerTodo()
        {
            return db.CitasVeterinarios.Select(c => c);
        }
    }
}