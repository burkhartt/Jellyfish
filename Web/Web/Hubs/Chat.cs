using Events;
using Events.Handler;
using Microsoft.AspNet.SignalR;

namespace Web.Hubs {
    public class ChatHub : Hub, IHandleDomainEvents<GoalCreatedEvent> {
        public void Send(string message) {
            
        }

        public void Handle(GoalCreatedEvent @event) {
            var context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            context.Clients.All.addMessage("hey");
        }
    }
}