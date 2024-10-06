﻿using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using BusinessLogic.Interfaces;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.Repository
{
    internal class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        internal TelegramBotDbContext context;
        internal DbSet<TEntity> dbSet;

        public Repository(TelegramBotDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> exp) => await dbSet.AnyAsync(exp);
        public virtual async Task<TEntity?> GetByIDAsync(object id) => await dbSet.FindAsync(id);
        public async virtual Task InsertAsync(TEntity entity) => await dbSet.AddAsync(entity);
        public async virtual Task<bool> AnyAsync() => await dbSet.AnyAsync();
        public async virtual Task AddRangeAsync(IEnumerable<TEntity> entities) => await dbSet.AddRangeAsync(entities);
        public virtual void Delete(object id)
        {
            TEntity? entityToDelete = dbSet.Find(id);
            if (entityToDelete != null)
                Delete(entityToDelete);
        }
        public async virtual Task DeleteAsync(object id)
        {
            TEntity? entityToDelete = await dbSet.FindAsync(id);
            if (entityToDelete != null)
                Delete(entityToDelete);
        }
        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }
        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }
        public async Task SaveAsync() => await context.SaveChangesAsync();
        public async Task<IEnumerable<TEntity>> GetListBySpec(ISpecification<TEntity> specification)
        {
            return await ApplySpecification(specification).ToListAsync();
        }
        public async Task<TEntity?> GetItemBySpec(ISpecification<TEntity> specification)
        {
            return await ApplySpecification(specification).FirstOrDefaultAsync();
        }
        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> specification)
        {
            var evaluator = new SpecificationEvaluator();
            return evaluator.GetQuery(dbSet, specification);
        }
        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> exp) => await dbSet.CountAsync(exp);
    }
}