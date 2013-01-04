using System;

namespace Web.Repositories {
    public interface IRepository<T> {
        T FindById(Guid id);
        void Update(T model);
        void Delete(Guid id);
        void Create(T model);
    }
    
    public class Repository<T> : IRepository<T> {
        public T FindById(Guid id) {
            return default(T);
        }

        public void Update(T model) {}
        public void Delete(Guid id) {}
        public void Create(T model) {}
    }
}