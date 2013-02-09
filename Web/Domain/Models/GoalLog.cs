using System;
using Entities;

namespace Domain.Models {
    public class GoalLog {
        public Account Account { get; private set; }
        public string Data { get; set; }
        public string Event { get; set; }
        public DateTime EventDate { get; set; }
        public string Message { get; private set; }

        public void SetMessage(string message) {
            Message = message;
        }
    }
}