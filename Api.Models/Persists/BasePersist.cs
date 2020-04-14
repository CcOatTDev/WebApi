using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Models.Persists
{
    public abstract class BasePersist<T, U> where T : class where U : DbContext, IDisposable
    {
        #region Members
        protected readonly DbContext context;
        protected readonly DbSet<T> dbSet;
        #endregion

        #region Contructors
        public BasePersist(DbContext context)
        {
            this.context = context;

            this.context.Configuration.ProxyCreationEnabled = false;
            this.context.Configuration.AutoDetectChangesEnabled = false;
            this.context.Configuration.ValidateOnSaveEnabled = false;

            dbSet = this.context.Set<T>();
        }
        #endregion

        #region Methods
        public U GetContext()
        {
            return (U)context;
        }

        public IQueryable<T> Get()
        {
            return dbSet;
        }

        public T GetById(object id)
        {
            return dbSet.Find(id);
        }

        public void Add(T newObj)
        {
            dbSet.Add(newObj);
        }

        public int Count()
        {
            return dbSet.Count();
        }

        public void Edit(object id, T updateObj)
        {
            dbSet.Attach(updateObj);
            context.Entry(updateObj).State = EntityState.Modified;
        }

        public void Delete(object id, T deleteObject)
        {
            dbSet.Attach(deleteObject);
            context.Entry(deleteObject).State = EntityState.Deleted;
        }

        public void Save()
        {
            try
            {
                context.SaveChanges();
            }

            catch (DbEntityValidationException e)
            {
                string message = "";

                foreach (var eve in e.EntityValidationErrors)
                {
                    message += string.Format("*** Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name,
                        eve.Entry.State);

                    foreach (var ve in eve.ValidationErrors)
                    {
                        message += string.Format("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName,
                            ve.ErrorMessage);
                    }
                }

                throw new Exception(message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Dispose()
        {
            context.Dispose();
        }
        #endregion
    }
}
