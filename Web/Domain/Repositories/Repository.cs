using System;
using System.Collections.Generic;
using Database;

namespace Domain.Repositories {
    public class Repository<T> : IRepository<T> {
        private readonly IDatabase database;

        public Repository(IDatabase database) {
            this.database = database;
        }

        public virtual T FindById(Guid id) {
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