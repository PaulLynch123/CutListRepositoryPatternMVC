using CutList.DataAccess.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CutList.DataAccess.Data.Repository
{
    //allowing me to pass models into each repository (where T : class)
    public class Repository<T> : IRepository<T> where T : class
    {
        //create the context with deault DBContext class
        protected readonly DbContext Context;
        //interal DBSet with the generic class inside it
        internal DbSet<T> dbSet;

        //initialise DbContext with dependency injection
        public Repository(DbContext context)
        {
            Context = context;
            this.dbSet = context.Set<T>();
        }

        //add by the model object
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        //add by model object id
        public T Get(int id)
        {
            return dbSet.Find(id);
        }

        //getAll for DataTables. 
        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            //getAll with a filter
            if(filter != null)
            {
                query = query.Where(filter);
            }
            //SEARCH (Comma seperated to allow for multiple individual seraches together) --eager loading--
            if(includeProperties != null)
            {
                //remove empty entries, seperate by comma, then add each to the query one by one
                foreach(var includeProperty in includeProperties.Split(new char[] { ','}, StringSplitOptions.RemoveEmptyEntries) )
                {
                    query = query.Include(includeProperty);
                }
            }
            //then we orderBy
            if(orderBy != null)
            {
                return orderBy(query).ToList();
            }
            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            //getAll with a filter
            if (filter != null)
            {
                query = query.Where(filter);
            }
            //include properties (Comma seperated to allow for multiple) --eager loading--
            if (includeProperties != null)
            {
                //remove empty entries, seperate by comma, then add each to the query one by one
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            //return the first 
            return query.FirstOrDefault();
        }

        public void Remove(int id)
        {
            T entityToRemove = dbSet.Find(id);
            Remove(entityToRemove);
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }
    }
}
