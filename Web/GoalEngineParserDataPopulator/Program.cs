using System;
using System.IO;
using System.Linq;
using GoalEngine;

namespace GoalEngineParserDataPopulator {
    public class Program {
        private static void Main(string[] args) {
            var again = "y";

            while (again.ToLower().Contains("y")) {
                Console.Clear();
                Console.WriteLine("Enter a goal");
                var goal = Console.ReadLine();
                var goalParser = new GoalParser();
                var result = goalParser.Parse(goal);
                Console.WriteLine(Line());
                Console.WriteLine("Quantity: " + GetQuantity(result.Quantity));
                Console.WriteLine("Direction: " + GetDirectionName(result.Direction));
                Console.WriteLine("Deadline: " + GetDeadline(result.Deadline));
                Console.WriteLine(Line());
                Console.WriteLine("Does this look correct to you? (Y or N)");
                var input = Console.ReadLine();
                if (input.ToLower().Contains("y")) {
                    Console.WriteLine("Yay!");
                }
                else {
                    LogFailedAttempt(goal);
                }
                Console.WriteLine("Again? (Y or N)");
                again = Console.ReadLine();
            }
        }

        private static void LogFailedAttempt(string goal) {
            using (var file = new StreamWriter(@"C:\Users\Public\FailedGoals.txt", true)) {
                file.WriteLine(goal);
            }
        }

        private static string GetQuantity(decimal? quantity) {
            return quantity.HasValue ? quantity.Value.ToString() : "N/A";
        }

        private static string Line() {
            return "--------------------------------------";
        }

        private static string GetDeadline(DateTime? deadline) {
            return deadline.HasValue ? deadline.Value.ToString("MMMM d, yyyy") : "N/A";
        }

        private static string GetDirectionName(GoalDirection direction) {
            if (direction == GoalDirection.Ascending)
                return "Ascending";
            if (direction == GoalDirection.Descending)
                return "Descending";
            return "N/A";
        }
    }
}