using System.Web.Mvc;
using Web.Authentication;

namespace Web.Config {
    public abstract class BasePage : WebViewPage {
        public new virtual Principal User {
            get { return base.User as Principal; }
        }

        protected override void InitializePage() {
            ViewBag.Title = "Goals";
        }
    }

    public abstract class BasePage<T> : WebViewPage<T> {
        public new virtual Principal User {
            get { return base.User as Principal; }
        }

        protected override void InitializePage() {
            ViewBag.Title = "Goals";
        }
    }
}