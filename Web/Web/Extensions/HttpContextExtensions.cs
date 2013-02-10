using System.Collections.Generic;
using System.Web;
using Domain.Models;
using Domain.Models.Goals;
using Entities;
using Web.Models;

namespace Web.Extensions {
    public static class HttpContextExtensions {
        public static AccountView Account(this HttpContextBase httpContextBase) {
            return (AccountView) httpContextBase.Items["Account"];
        }

        public static Site Site(this HttpContextBase httpContextBase) {
            return (Site) httpContextBase.Items["Site"];
        }

        public static IEnumerable<Group> Groups(this HttpContextBase httpContextBase) {
            return (IEnumerable<Group>) httpContextBase.Items["Groups"];
        }

        public static IEnumerable<Goal> Goals(this HttpContextBase httpContextBase) {
            return (IEnumerable<Goal>) httpContextBase.Items["Goals"];
        }

        public static IEnumerable<Account> GroupMembers(this HttpContextBase httpContextBase) {
            return (IEnumerable<Account>)httpContextBase.Items["GroupMembers"];
        }

        public static Group CurrentGroup(this HttpContextBase httpContextBase) {
            return (Group) httpContextBase.Items["CurrentGroup"];
        }

        public static IEnumerable<Account> Friends(this HttpContextBase httpContextBase) {
            return (IEnumerable<Account>) httpContextBase.Items["Friends"];
        }
    }
}