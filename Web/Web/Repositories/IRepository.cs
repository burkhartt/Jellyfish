using System;

namespace Web.Repositories {
    public interface IRepository<T> {
        T FindById(Guid id);
        void Update(T model);
        void Delete(Guid id);
    }
    
    public class Repository<T> : IRepository<T> {
        public T FindById(Guid id) {
            return default(T);
        }

        public void Update(T model) {}
        public void Delete(Guid id) {}
    }
}