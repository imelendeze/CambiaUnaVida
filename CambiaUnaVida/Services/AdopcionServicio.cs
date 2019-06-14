using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CambiaUnaVida.Models.Repository;
using CambiaUnaVida.Models.Dominio;
using CambiaUnaVida.Models.Repository.Infrastructure;
using CambiaUnaVida.Models.Repository.EntityRepository;
using System.Linq.Expressions;
using CambiaUnaVida.Models;

namespace CambiaUnaVida.Services
{
    public class AdopcionServicio
    {
        private IUnitOfWork _iuow = null;
        private ApplicationDbContext context = null;
        private IGeneric<Adopcion> _iGen = null;

        public AdopcionServicio(ApplicationDbContext c)
        {
            context = c;
        }

        public IUnitOfWork iuow { get { return _iuow = _iuow ?? new UnitOfWork(context); } }
        public IGeneric<Adopcion> iGen { get { return _iGen = _iGen ?? new AdopcionRepository(context); } }

        public bool guardar(Adopcion ent)
        {
            try
            {
                iGen.crear(ent);
                iuow.commit();

                return true;
            }
            catch (Exception e)
            {
                iuow.rollback();
                return false;
            }
        }

        public List<Adopcion> obtenerPor(Expression<Func<Adopcion, bool>> condicion)
        {
            try
            {
                return iGen.encontrarPor(condicion).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Adopcion obtenerPorID(Adopcion ent)
        {
            try
            {
                Adopcion entAux = iGen.obtenerPorId(ent);

                if (entAux != null)
                    return entAux;
                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public void eliminar(Adopcion ent)
        {
            try
            {
                iGen.eliminar(ent);
                iuow.commit();
            }
            catch (Exception e)
            {
                iuow.rollback();
            }
        }

        public void actualizar(Adopcion ent)
        {
            try
            {
                iGen.actualizar(ent);
                iuow.commit();
            }
            catch (Exception e)
            {
                iuow.rollback();
            }
        }

        public List<Adopcion> obtenerTodo()
        {
            try
            {
                return iGen.obtenerTodo().ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}