using DotNetMvcDemo.Data;
using DotNetMvcDemo.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;

namespace DotNetMvcDemo.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private IDbSet<T> _entities;
        private string _errorMessage = string.Empty;
        private bool _isDisposed;

        public ApplicationDbContext Context { get; set; }
        public virtual IQueryable<T> Table => Entities;

        protected virtual IDbSet<T> Entities => _entities ?? (_entities = Context.Set<T>());
        public GenericRepository(IUnitOfWork<ApplicationDbContext> unitOfWork)
            : this(unitOfWork.Context)
        {
        }
        public GenericRepository(ApplicationDbContext context)
        {
            _isDisposed = false;
            Context = context;
        }

        public void Dispose()
        {
            Context?.Dispose();
            _isDisposed = true;
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<string> includes = null)
        {
            var query = Table;

            if (expression != null)
            {
                query = query.Where(expression);
            }

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            var data = query.ToList();
            return data;
        }

        public virtual T GetById(Expression<Func<T, bool>> expression, List<string> includes = null)
        {
            var query = Table;
            if (expression != null)
            {
                query = query.Where(expression);
            }

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            }

            var data = query.FirstOrDefault();
            return data;
        }


        public virtual void Insert(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                Entities.Add(entity);

                if (Context == null || _isDisposed)
                    Context = new ApplicationDbContext();

            }
            catch (DbEntityValidationException dbEx)
            {
                ManageException(dbEx);
            }
        }

        public void BulkInsert(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                {
                    throw new ArgumentNullException(nameof(entities));
                }
                Context.Configuration.AutoDetectChangesEnabled = false;
                Context.Set<T>().AddRange(entities);
                Context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                ManageException(dbEx);
            }
        }

        public virtual void Update(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));
                if (Context == null || _isDisposed)
                    Context = new ApplicationDbContext();
                SetEntryModified(entity);

            }
            catch (DbEntityValidationException dbEx)
            {
                ManageException(dbEx);

            }
        }
        public virtual void Delete(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));
                if (Context == null || _isDisposed)
                    Context = new ApplicationDbContext();
                Entities.Remove(entity);

            }
            catch (DbEntityValidationException dbEx)
            {
                ManageException(dbEx);
            }
        }
        public virtual void SetEntryModified(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

        public void ManageException(DbEntityValidationException dbEx)
        {
            foreach (var validationErrors in dbEx.EntityValidationErrors)
            {
                foreach (var validationError in validationErrors.ValidationErrors)
                {
                    _errorMessage += Environment.NewLine +
                                     $"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}";
                }
            }

            throw new Exception(_errorMessage, dbEx);
        }

    }
}