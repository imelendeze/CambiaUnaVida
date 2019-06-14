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
    public class ApplicationUserServicio
    {
        private IUnitOfWork _iuow = null;
        private ApplicationDbContext context= null;
        private IGeneric<ApplicationUser> _iGen = null;

        public ApplicationUserServicio(ApplicationDbContext c)
        {
            context = c;
        }

        public IUnitOfWork iuow { get { return _iuow = _iuow ?? new UnitOfWork(context); } }
        public IGeneric<ApplicationUser> iGen { get { return _iGen = _iGen ?? new ApplicationUserRepository(context); } }

        public bool guardar(ApplicationUser ent)
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

        public List<ApplicationUser> obtenerPor(Expression<Func<ApplicationUser, bool>> condicion)
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

        public ApplicationUser obtenerPorID(ApplicationUser ent)
        {
            try
            {
                ApplicationUser entAux = iGen.obtenerPorId(ent);

                if (entAux != null)
                    return entAux;
                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public void eliminar(ApplicationUser ent)
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

        public void actualizar(ApplicationUser ent)
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

        public List<ApplicationUser> obtenerTodo()
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