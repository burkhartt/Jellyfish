using System;
using System.Web;

namespace Web.Repositories {
    public interface IAccountSessionRepository {
        void SetCurrentId(Guid id);
        Guid GetCurrentId();
        void Clear();
    }

    public class AccountSessionRepository : IAccountSessionRepository {
        public void SetCurrentId(Guid id) {
            HttpContext.Current.Session["AccountId"] = id;
        }

        public Guid GetCurrentId() {
            if (HttpContext.Current.Session == null) {
                return Guid.Empty;
            }

            var accountId = HttpContext.Current.Session["AccountId"];

            if (accountId == null) {
                return Guid.Empty;
            }

            return (Guid) accountId;
        }

        public void Clear() {
            HttpContext.Current.Session.Remove("AccountId");
        }
    }
}