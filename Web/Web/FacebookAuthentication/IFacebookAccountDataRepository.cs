using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Domain.Models.Accounts;
using Domain.Repositories;
using Newtonsoft.Json;
using Web.Models;
using Web.Repositories;

namespace Web.FacebookAuthentication {
    public class FacebookAccountRepository : IAccountRepository {
        private readonly IAccountRepository accountRepository;
        private readonly IFacebookDataRepository facebookDataRepository;

        public FacebookAccountRepository(IAccountRepository accountRepository,
                                         IFacebookDataRepository facebookDataRepository) {
            this.accountRepository = accountRepository;
            this.facebookDataRepository = facebookDataRepository;
        }

        public Account FindById(Guid id) {
            var account = accountRepository.FindById(id);
            if (account == null) return new Account(Guid.NewGuid());

            if (!account.IsAFacebookAccount) {
                return account;
            }

            var request = WebRequest.Create("https://graph.facebook.com/me?fields=id,first_name,last_name,picture.width(800).height(800)&access_token=" + facebookDataRepository.GetAccessToken());
            
            using (var reader = new StreamReader(request.GetResponse().GetResponseStream(), Encoding.ASCII)) {
                var result = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<FacebookAccount>(result, new FacebookAccountJsonDeserialization(account));
            }
        }

        public IEnumerable<Account> All() {
            return accountRepository.All();
        }

        public void Update(Account model) {
            accountRepository.Update(model);
        }

        public void Delete(Guid id) {
            accountRepository.Delete(id);
        }

        public void Create(Account model) {
            accountRepository.Create(model);
        }

        public void AddInvitedAccount(Account account) {
            accountRepository.AddInvitedAccount(account);
        }

        public Account GetByEmailAddressAndPassword(string emailAddress, string password) {
            return accountRepository.GetByEmailAddressAndPassword(emailAddress, password);
        }

        public IEnumerable<Account> GetAllUnconfirmedAccounts() {
            return accountRepository.GetAllUnconfirmedAccounts();
        }

        public Account GetByFacebookId(int facebookId) {
            return accountRepository.GetByFacebookId(facebookId);
        }
    }
}