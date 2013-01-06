using System;
using System.Collections.Generic;
using Web.Database;

namespace Web.Repositories {
    public interface IRepository<T> {
        T FindById(Guid id);
        IEnumerable<T> All();
        void Update(T model);
        void Delete(Guid id);
        void Create(T model);
    }
    
    public class Repository<T> : IRepository<T> {
        private readonly IDatabase database;

        public Repository(IDatabase database) {
            this.database = database;
        }

        public T FindById(Guid id) {
            return database.GetTheDatabase()[TableName()].FindById(id);
        }

        public IEnumerable<T> All() {
            return database.GetTheDatabase()[TableName()].All().ToList<T>();
        }

        public void Update(T model) {
            database.GetTheDatabase()[TableName()].Update(model);
        }
        
        public void Delete(Guid id) {}
        
        public void Create(T model) {
            database.GetTheDatabase()[TableName()].Insert(model);
        }

        private static string TableName() {
            return typeof(T).Name;
        }
    }
}