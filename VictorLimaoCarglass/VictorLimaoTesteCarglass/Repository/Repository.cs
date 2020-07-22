using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EstruturarRepository.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public void Add(T obj)
        {
            using (var dbContext = new DbContexto())
            {
                DbSet<T> table = dbContext.Set<T>();
                table.Add(obj);
                dbContext.SaveChanges();
            }
        }

        public void Deletar(object id)
        {
            using (var dbContext = new DbContexto())
            {
                DbSet<T> table = dbContext.Set<T>();
                T obj = table.Find(id);
                table.Remove(obj);
                dbContext.SaveChanges();
            }
        }

        public List<T> GetAll()
        {
            using (var dbContext = new DbContexto())
            {
                DbSet<T> table = dbContext.Set<T>();
                return table.ToList();
            }
        }

        public T GetById(object id)
        {
            using (var dbContext = new DbContexto())
            {
                DbSet<T> table = dbContext.Set<T>();
                return table.Find(id);
            }
        }

        public void Save()
        {
            using (var dbContext = new DbContexto())
            {
                DbSet<T> table = dbContext.Set<T>();
                dbContext.SaveChanges();
            }
        }

        public void Update(T obj)
        {
            using (var dbContext = new DbContexto())
            {
                DbSet<T> table = dbContext.Set<T>();
                table.Attach(obj);
                dbContext.Entry(obj).State = EntityState.Modified;
                dbContext.SaveChanges();
            }
        }

    }
}