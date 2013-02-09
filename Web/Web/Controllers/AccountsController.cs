using System;
using System.Web.Mvc;
using Domain.Repositories;
using Web.Filters;

namespace Web.Controllers {
    [Authorized]
    public class AccountsController : Controller {
        private readonly IAccountRepository accountRepository;

        public AccountsController(IAccountRepository accountRepository) {
            this.accountRepository = accountRepository;
        }

        public ViewResult Index(Guid id) {
            return View(accountRepository.FindById(id));
        }
    }
}