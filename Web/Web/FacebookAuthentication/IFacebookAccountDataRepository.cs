using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
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
            if (account == null) return new Account();

            if (!account.IsAFacebookAccount) {
                return account;
            }

            var request = WebRequest.Create("https://graph.facebook.com/me?access_token=" + facebookDataRepository.GetAccessToken());
            
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

    public class FacebookAccountJsonDeserialization : CustomCreationConverter<FacebookAccount> {
        private readonly IAccount account;

        public FacebookAccountJsonDeserialization(IAccount account) {
            this.account = account;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
                                        JsonSerializer serializer) {
            var mappedObj = new FacebookAccount(account);
            //get an array of the object's props so I can check if the JSON prop s/b mapped to it
            var objProps = objectType.GetProperties().Select(p => p.Name.ToLower()).ToArray();

            //loop through my JSON string
            while (reader.Read()) {
                if (reader.TokenType != JsonToken.PropertyName) continue;
                var readerValue = reader.Path.Replace("_", "").ToLower();

                if (readerValue == "id") {
                    readerValue = "facebookid";
                }

                if (reader.Read() && objProps.Contains(readerValue)) {
                    //get the property info and set the Mapped object's property value
                    var pi = mappedObj.GetType()
                                      .GetProperty(readerValue,
                                                   BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    var convertedValue = Convert.ChangeType(reader.Value, pi.PropertyType);
                    pi.SetValue(mappedObj, convertedValue, null);
                }
            }
            return mappedObj;
        }

        public override FacebookAccount Create(Type objectType) {
            return new FacebookAccount();
        }
    }
}