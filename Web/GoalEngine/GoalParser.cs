using System.Collections.Generic;
using System.Linq;

namespace GoalEngine {
    public class GoalParser {
        public ParsedGoal Parse(string goal) {
            var elements = goal.Split(' ');
            var parsedElements = new List<ParsedGoalElement>();

            for (var i = 0; i < elements.Length; i++) {
                var parsedGoalElement = new ParsedGoalElement(elements[i]);
                
                if (i > 0) {
                    parsedGoalElement.SetPrevious(parsedElements[i - 1]);
                }

                parsedElements.Add(parsedGoalElement);
            }

            var parsedGoalElements = new ParsedGoalElements(parsedElements);

            return new ParsedGoal {
                Deadline = parsedGoalElements.GetDeadline(),
                Direction = parsedGoalElements.GetDirection(),
                Quantity = parsedGoalElements.GetQuantity()
            };
        }        
    }
}