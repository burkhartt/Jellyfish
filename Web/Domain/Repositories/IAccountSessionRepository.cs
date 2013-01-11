using System;

namespace Domain.Repositories {
    public interface IAccountSessionRepository {
        void SetCurrentId(Guid id);
        Guid GetCurrentId();
        void Clear();
    }
}