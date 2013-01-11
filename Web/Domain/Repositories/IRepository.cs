using System;
using System.Collections.Generic;

namespace Domain.Repositories {
    public interface IRepository<T> {
        T FindById(Guid id);
        IEnumerable<T> All();
        void Update(T model);
        void Delete(Guid id);
        void Create(T model);
    }
}