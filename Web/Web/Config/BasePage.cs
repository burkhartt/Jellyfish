using System.Web.Mvc;

namespace Web.Config {
    public abstract class BasePage : WebViewPage {
        
    }

    public abstract class BasePage<T> : WebViewPage<T> {
        protected override void InitializePage() {
            ViewBag.Title = "Goals";
        }        
    }
}