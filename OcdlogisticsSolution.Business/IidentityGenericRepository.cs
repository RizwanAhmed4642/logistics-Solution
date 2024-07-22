using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcdlogisticsSolution.Business
{
    public interface IdentityIgenericRepository<T> where T : class
    {
        IEnumerable<T> List { get; }
        void add(T entity);
        void update(T entity);
        T FindById(decimal id);
        void delete(decimal id);
    }
}