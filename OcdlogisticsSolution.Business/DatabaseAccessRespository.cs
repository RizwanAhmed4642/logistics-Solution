using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OcdlogisticsSolution.DomainModels.Models.Entity_Models;
namespace OcdlogisticsSolution.Business
{
    public class Database
    {
        public static OcdlogisticsEntities Context { get; set; }

        public Database()
        {
            Context = new OcdlogisticsEntities();
        }
        public Database(OcdlogisticsEntities context)
        {
            Context = context;
        }
        public static class Entity<T> where T : class
        {
            public static async Task<bool> AddAsync(T @object)
            {
                using (OcdlogisticsEntities OcdlogisticsEntities = new OcdlogisticsEntities())
                {
                    OcdlogisticsEntities.Entry<T>(@object).State = System.Data.Entity.EntityState.Added;
                    var isAffceted = await OcdlogisticsEntities.SaveChangesAsync();
                    return isAffceted > 0;
                }
            }
            public static async Task<bool> UpdateAsync(T @object, OcdlogisticsEntities context)
            {
                context.Entry<T>(@object).State = System.Data.Entity.EntityState.Modified;
                var isAffceted = await context.SaveChangesAsync();
                return isAffceted > 0;

            }

            public static async Task<bool> UpdateAsync(T @object)
            {
                using (OcdlogisticsEntities OcdlogisticsEntities = new OcdlogisticsEntities())
                {
                    OcdlogisticsEntities.Entry<T>(@object).State = System.Data.Entity.EntityState.Modified;
                    var isAffceted = await OcdlogisticsEntities.SaveChangesAsync();
                    return isAffceted > 0;
                }
            }

            public static async Task<bool> RemoveAsync(T @object, OcdlogisticsEntities context)
            {
                context.Entry<T>(@object).State = System.Data.Entity.EntityState.Deleted;
                var isAffceted = await context.SaveChangesAsync();
                return isAffceted > 0;

            }
        }
    }
}
