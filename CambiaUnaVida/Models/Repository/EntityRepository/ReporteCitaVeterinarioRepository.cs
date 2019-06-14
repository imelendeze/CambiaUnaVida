using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using CambiaUnaVida.Models.Dominio;

namespace CambiaUnaVida.Models.Repository.EntityRepository
{
    public class ReporteCitaVeterinarioRepository : IGeneric<ReporteCitaVeterinario>
    {
        ApplicationDbContext db;

        public ReporteCitaVeterinarioRepository(ApplicationDbContext context)
        {
            db = context;
        }
        public void actualizar(ReporteCitaVeterinario Tentity)
        {
            ReporteCitaVeterinario TentityAux = obtenerPorId(Tentity);

            if (TentityAux != null)
            {
                db.Entry<ReporteCitaVeterinario>(TentityAux).State = EntityState.Detached;
                db.Entry<ReporteCitaVeterinario>(TentityAux).State = EntityState.Modified;
            }
        }

        public void crear(ReporteCitaVeterinario Tentity)
        {
            db.ReportesCitasVeterinario.Add(Tentity);
        }

        public void eliminar(ReporteCitaVeterinario Tentity)
        {
            db.Entry<ReporteCitaVeterinario>(obtenerPorId(Tentity)).State = EntityState.Deleted;
        }

        public IQueryable<ReporteCitaVeterinario> encontrarPor(Expression<Func<ReporteCitaVeterinario, bool>> condicion)
        {
            return db.ReportesCitasVeterinario.Where(condicion).Select(c => c);
        }

        public ReporteCitaVeterinario obtenerPorId(ReporteCitaVeterinario Tentity)
        {
            return db.ReportesCitasVeterinario.FirstOrDefault(c => c.id == Tentity.id);
        }

        public IQueryable<ReporteCitaVeterinario> obtenerTodo()
        {
            return db.ReportesCitasVeterinario.Select(c => c);
        }
    }
}