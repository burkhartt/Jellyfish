using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Events;
using Events.Bus;

namespace Cms.Controllers {
    public class HomeController : Controller {
        private readonly ICommandBus commandBus;

        public HomeController(ICommandBus commandBus) {
            this.commandBus = commandBus;
        }

        public ActionResult Index() {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string nothing) {
            commandBus.Send(new CreateAccountCommand {FirstName = "Tim", LastName = "Burkhart"});

            return View();
        }
    }

    public class CreateAccountCommand : ICommand {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public interface ICommand {}

    public interface ICommandBus {
        void Send<T>(T command) where T : ICommand;
    }

    public class CommandBus : ICommandBus {
        private readonly IComponentContext componentContext;

        public CommandBus(IComponentContext componentContext) {
            this.componentContext = componentContext;
        }

        public void Send<T>(T command) where T : ICommand {
            var commandHandler = (ICommandHandler<T>) componentContext.Resolve(typeof (ICommandHandler<T>));
            commandHandler.Handle(command);
        }
    }

    public interface ICommandHandler<in T> where T : ICommand {
        void Handle(T command);
    }

    public class CreateAccountCommandHandler : ICommandHandler<CreateAccountCommand> {
        private readonly IDomainRepository domainRepository;

        public CreateAccountCommandHandler(IDomainRepository domainRepository) {
            this.domainRepository = domainRepository;
        }

        public void Handle(CreateAccountCommand command) {
            var account = new Account(Guid.NewGuid());
            account.SetName(command.FirstName, command.LastName);
            domainRepository.Save(account);
        }
    }

    public class Account : AggregateRoot {
        public Account(Guid id) {
            Apply(new AccountCreatedEvent {AggregateRootId = id});
        }

        public void SetName(string firstName, string lastName) {
            Apply(new AccountNameSetEvent {FirstName = firstName, LastName = lastName});
        }

        public void OnAccountCreated(AccountCreatedEvent accountCreatedEvent) {
            Id = accountCreatedEvent.AggregateRootId;
        }
    }

    public abstract class AggregateRoot {
        private readonly Queue<DomainEvent> uncommittedEvents = new Queue<DomainEvent>();
        public Guid Id { get; protected set; }
        public char LastEventSequence { get; protected set; }

        public ReadOnlyCollection<DomainEvent> UncommittedEvents {
            get { return new ReadOnlyCollection<DomainEvent>(uncommittedEvents.ToList()); }
        }

        protected void Apply(DomainEvent domainEvent) {
            domainEvent.Sequence = ++LastEventSequence;
            ApplyEventToDomainEntityState(domainEvent);

            domainEvent.AggregateRootId = Id;
            domainEvent.EventDate = DateTime.Now;
            uncommittedEvents.Enqueue(domainEvent);
        }

        private void ApplyEventToDomainEntityState(DomainEvent domainEvent) {
            var domainEventType = domainEvent.GetType();
            var domainEventTypeName = domainEventType.Name;
            var aggregateRootType = GetType();

            var eventHandlerMethodName = GetEventHandlerMethodName(domainEventTypeName);
            var methodInfo = aggregateRootType.GetMethod(eventHandlerMethodName,
                                                         BindingFlags.Instance | BindingFlags.Public |
                                                         BindingFlags.NonPublic, null, new[] {domainEventType}, null);

            if (methodInfo != null && EventHandlerMethodInfoHasCorrectParameter(methodInfo, domainEventType)) {
                methodInfo.Invoke(this, new[] {domainEvent});
            }
        }

        private static string GetEventHandlerMethodName(string domainEventTypeName) {
            var eventIndex = domainEventTypeName.LastIndexOf("Event");
            return "On" + domainEventTypeName.Remove(eventIndex, 5);
        }

        private static bool EventHandlerMethodInfoHasCorrectParameter(MethodInfo eventHandlerMethodInfo,
                                                                      Type domainEventType) {
            var parameters = eventHandlerMethodInfo.GetParameters();
            return parameters.Length == 1 && parameters[0].ParameterType == domainEventType;
        }
    }

    public interface IDomainRepository {
        void Save(AggregateRoot aggregateRoot);
    }

    public class DomainRepository : IDomainRepository {
        private readonly List<AggregateRoot> aggregateRoots;
        private readonly IEventBus eventBus;

        public DomainRepository(IEventBus eventBus) {
            this.eventBus = eventBus;
            aggregateRoots = new List<AggregateRoot>();
        }

        public void Save(AggregateRoot aggregateRoot) {
            aggregateRoots.Add(aggregateRoot);
            
            foreach (var uncommittedEvent in aggregateRoot.UncommittedEvents) {
                eventBus.Send(uncommittedEvent);
            }
        }
    }
}