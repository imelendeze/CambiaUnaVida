using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CambiaUnaVida.Models.Repository
{
    public interface IGeneric<T> where T : class
    {
        void crear(T Tentity);
        void actualizar(T Tentity);
        void eliminar(T Tentity); //En lugar de declarar un int, le mando el objeto ya que en la tabla de AspNetUsers, el id es un string.
        T obtenerPorId(T Tentity); //En lugar de declarar un int, le mando el objeto ya que en la tabla de AspNetUsers, el id es un string.
        IQueryable<T> encontrarPor(Expression<Func<T, bool>> condicion);
        IQueryable<T> obtenerTodo();
    }
}
