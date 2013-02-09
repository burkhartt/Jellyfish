using System;
using System.Collections.Generic;
using Database;
using Domain.Models;
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

        public IEnumerable<GoalLog> GetAllById(Guid id) {
            IEnumerable<GoalLog> goalLogs = database.GetTheDatabase().GoalLog.FindAllByGoalId(id).ToList<GoalLog>();

            foreach (var goalLog in goalLogs) {
                var type = Type.GetType(goalLog.Event);
                var goalDomainEvent = (GoalDomainEvent) JsonConvert.DeserializeObject(goalLog.Data, type);
                var account = accountRepository.FindById((Guid)goalDomainEvent.AccountId);
                goalLog.SetMessage(goalDomainEvent.GetMessage(account));
            }

            return goalLogs;
        }
    }
}