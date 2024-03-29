﻿using System;
using System.Collections.Generic;
using System.Linq;
using Database;
using Domain.Models.Goals;

namespace Domain.Repositories {
    internal class GoalRepository : Repository<Goal>, IGoalRepository {
        private readonly IDatabase database;
        private readonly IGoalLogRepository goalLogRepository;

        public GoalRepository(IDatabase database, IGoalLogRepository goalLogRepository) : base(database) {
            this.database = database;
            this.goalLogRepository = goalLogRepository;
        }

        public IEnumerable<Goal> AllByGroupId(Guid groupId, Guid parentGoalId) {
            return AllByGroupId(groupId).Where(x => x.BucketId == parentGoalId);
        }

        public IEnumerable<Goal> AllByGroupId(Guid groupId) {
            IEnumerable<GroupGoal> groupGoals = database.GetTheDatabase().GroupGoals.FindAllByGroupId(groupId).ToList<GroupGoal>();
            var goalIds = groupGoals.Select(@group => @group.GoalId).ToList();
            return database.GetTheDatabase().Goals.FindAllById(goalIds).ToList<Goal>();
        }

        public Goal GetById(Guid id) {
            var goal = (Goal)database.GetTheDatabase().Goals.FindById(id);
            var logs = goalLogRepository.GetAllById(id).ToList();

            if (goal != null && !string.IsNullOrEmpty(goal.Type) && goal.Type.Equals("Quantitative", StringComparison.OrdinalIgnoreCase)) {
                var quantitiativeGoal = (QuantitativeGoal)database.GetTheDatabase().Goals.FindById(id);
                quantitiativeGoal.Logs = logs;
                return quantitiativeGoal;
            }

            if (goal != null) {
                goal.Logs = logs;
            }            

            return goal;
        }
    }
}