using Autofac;

namespace Database {
    public class DatabaseModule : Module {
        protected override void Load(ContainerBuilder builder) {
            builder.RegisterType(typeof (Database)).As(typeof (IDatabase)).SingleInstance();
        }
    }
}