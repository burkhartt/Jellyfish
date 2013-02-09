using Autofac;
using Domain.Repositories;

namespace Domain {
    public class DomainModule : Module {
        protected override void Load(ContainerBuilder builder) {
            builder.RegisterType<GoalRepository>().As<IGoalRepository>();
            builder.RegisterType<GroupRepository>().As<IGroupRepository>();
            builder.RegisterType<TaskRepository>().As<ITaskRepository>();
            builder.RegisterType<GoalTypeRepository>().As<IGoalTypeRepository>();
            builder.RegisterType<GoalLogRepository>().As<IGoalLogRepository>();
            builder.RegisterType<GroupGoalRepository>().As<IGroupGoalRepository>();
        }
    }
}