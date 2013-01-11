using System;
using System.Linq;
using System.Reflection;
using Domain.Models;
using Domain.Models.Accounts;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Web.Models;

namespace Web.FacebookAuthentication {
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
                if (readerValue == "picture.data.url") {
                    readerValue = "picture";
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