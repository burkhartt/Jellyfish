using System;
using System.Collections.Generic;

namespace CmsDomain.Repositories {
    public interface IRepository<T> where T : CmsEntity {
        T FindById(Guid id);
        IEnumerable<T> All();
        void Update(T model);
        void Delete(Guid id);
        void Create(T model);
    }
}