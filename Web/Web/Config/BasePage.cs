using System.Web.Mvc;
using System.Web.Security;
using Web.Authentication;
using Web.Repositories;

namespace Web.Config {
    public abstract class BasePage : WebViewPage {
        public Principal Principal { get; set; }

        public new virtual Principal User {
            get { return base.User as Principal; }
        }

        protected override void InitializePage() {
            ViewBag.Title = "Goals";
        }
    }

    public abstract class BasePage<T> : WebViewPage<T> {
        public Principal Principal { get; set; }

        public new virtual Principal User {
            get { return base.User as Principal; }
        }

        protected override void InitializePage() {
            ViewBag.Title = "Goals";
        }
    }
}