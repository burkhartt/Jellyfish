using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Domain.Repositories;
using Entities;
using Events;
using Events.Bus;
using Events.Friends;
using Newtonsoft.Json;
using ServiceStack.Text;
using Web.Models;
using Web.Repositories;

namespace Web.FacebookAuthentication {
    public class FacebookAccountRepository : IAccountRepository {
        private readonly IAccountRepository accountRepository;
        private readonly IFacebookDataRepository facebookDataRepository;
        private readonly IEventBus eventBus;

        public FacebookAccountRepository(IAccountRepository accountRepository,
                                         IFacebookDataRepository facebookDataRepository, IEventBus eventBus) {
            this.accountRepository = accountRepository;
            this.facebookDataRepository = facebookDataRepository;
            this.eventBus = eventBus;
        }

        public Account FindById(Guid id) {
            var account = accountRepository.FindById(id);
            if (account == null) return new Account(Guid.NewGuid());

            if (!account.IsAFacebookAccount) {
                return account;
            }

            var request = WebRequest.Create("https://graph.facebook.com/me?fields=friends,id,first_name,last_name,picture.width(800).height(800)&access_token=" + facebookDataRepository.GetAccessToken());
            
            using (var reader = new StreamReader(request.GetResponse().GetResponseStream(), Encoding.ASCII)) {                
                var result = reader.ReadToEnd();
                var act = JsonConvert.DeserializeObject<dynamic>(result);

                foreach (var friend in act.friends.data) {
                    var fullName = (string)friend.name;
                    var firstName = fullName.Substring(0, fullName.IndexOf(" ")).Trim();
                    var lastName = fullName.Substring(firstName.Length).Trim();
                    var facebookId = (long) friend.id;
                    var friendAccount = accountRepository.GetByFacebookId(facebookId);
                    if (friendAccount == null) {
                        var friendAccountId = Guid.NewGuid();
                        eventBus.Send(new FacebookFriendAccountRetrievedEvent { Id = friendAccountId, FacebookId = facebookId, FirstName = firstName, LastName = lastName, Picture = "/Content/img/fb-silhouette.jpg" });
                        friendAccount = accountRepository.GetByFacebookId(facebookId);
                    }
                    
                    eventBus.Send(new FacebookFriendFoundEvent { AccountId = account.Id, FriendId = friendAccount.Id });
                }                

                return new FacebookAccount {
                    FacebookId = act.id,
                    AccountConfirmed = true,
                    FirstName = act.first_name,
                    LastName = act.last_name,
                    Picture = act.picture.data.url,
                    Id = account.Id
                };
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

        public Account GetByFacebookId(long facebookId) {
            return accountRepository.GetByFacebookId(facebookId);
        }
    }    
}