using System;
using System.Collections.Generic;
using Web.Models;
using Web.Repositories;

namespace Web.Denormalizers {
    public class GoalDenormalizer : GenericDenormalizer<Goal> {
        public GoalDenormalizer(IRepository<Goal> repository) : base(repository) {}
    }
}