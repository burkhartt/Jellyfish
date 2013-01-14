using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GoalEngine {
    public class ParsedGoalElements : IEnumerable<ParsedGoalElement> {
        private readonly IEnumerable<ParsedGoalElement> elements;

        public ParsedGoalElements(IEnumerable<ParsedGoalElement> elements) {          
            this.elements = elements;
        }

        public decimal? Quantity { get; private set; }

        public IEnumerator<ParsedGoalElement> GetEnumerator() {
            return elements.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public DateTime? GetDeadline() {
            foreach (var element in elements.Where(element => element.IsADate)) {
                return element.GetDate();
            }

            return null;
        }

        public GoalDirection GetDirection() {
            foreach (var element in elements.Where(element => element.IsADirection)) {
                return element.GetDirection();
            }
            if (elements.Any(x => x.IsAQuantity)) {
                return GoalDirection.Ascending;
            }
            return GoalDirection.None;
        }

        public decimal? GetQuantity() {
            foreach (var element in elements.Where(element => element.IsAQuantity)) {
                return element.GetQuantity();
            }
            return null;
        }
    }
}