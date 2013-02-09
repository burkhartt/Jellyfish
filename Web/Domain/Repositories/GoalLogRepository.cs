using System;
using System.Collections.Generic;
using System.Linq;
using Database;
using Domain.Models;
using Entities;
using Events;
using Newtonsoft.Json;

namespace Domain.Repositories {
    internal class GoalLogRepository : IGoalLogRepository {
        private readonly IDatabase database;
        private readonly IAccountRepository accountRepository;

        public GoalLogRepository(IDatabase database, IAccountRepository accountRepository) {
            this.database = database;
            this.accountRepository = accountRepository;
        }

        public IEnumerable<string> GetAllById(Guid id) {
            IEnumerable<GoalLog> goalLogs = database.GetTheDatabase().GoalLog.FindAllByGoalId(id).ToList<GoalLog>();
            var accountIds = new HashSet<Guid>();
            var domainEvents = new List<GoalDomainEvent>();
            var accounts = new Dictionary<Guid, Account>();

            foreach (var goalLog in goalLogs) {
                var type = Type.GetType(goalLog.Event);
                var goalDomainEvent = (GoalDomainEvent)JsonConvert.DeserializeObject(goalLog.Data, type);
                accountIds.Add(goalDomainEvent.AccountId);
                domainEvents.Add(goalDomainEvent);
            }

            foreach (var accountId in accountIds) {
                accounts[accountId] = accountRepository.FindById(accountId);
            }

            return domainEvents.Select(domainEvent => domainEvent.GetMessage(accounts[domainEvent.AccountId])).ToList();
        }
    }
}