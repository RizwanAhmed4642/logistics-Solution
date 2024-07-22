
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcdlogisticsSolution.Business
{
    public class IdentityGenericRepository<T> : IdentityIgenericRepository<T> where T : class
    {
        public readonly RatsIdentityEntities context;
        private DbSet<T> entities;
        public IdentityGenericRepository(RatsIdentityEntities contexta)
        {
            this.context = contexta;
            entities = contexta.Set<T>();
        }
        public IEnumerable<T> List
        {
            get
            {
                return entities.AsEnumerable();
            }
        }

        public void add(T entity)
        {
            entities.Add(entity);
            context.SaveChanges();
        }


        public T FindById(decimal Id)
        {
            return entities.Find(Id);
        }

        public void update(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }
        public void delete(decimal Id)
        {
            var entity = entities.Find(Id);
            context.Entry(entity).State = EntityState.Deleted;
            context.SaveChanges();
        }
    }
}
