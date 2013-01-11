using Autofac;

namespace Email {
    public class EmailModule : Module {
        protected override void Load(ContainerBuilder builder) {
            builder.RegisterType(typeof (EmailSender)).As(typeof (IEmailSender));
        }
    }
}