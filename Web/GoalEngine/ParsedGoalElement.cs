using System;
using System.Collections.Generic;
using System.Linq;

namespace GoalEngine {
    public class ParsedGoalElement {
        private ParsedGoalElement previousElement;
        public ParsedGoalElement NextElement;
        private readonly string text;
        private readonly IEnumerable<string> wordsMeaningDate = new[] {"year","years"};
        private readonly IEnumerable<string> wordsMeaningLess = new[] {"less", "under", "lose"};
        private readonly IEnumerable<string> years;
        private readonly IEnumerable<string> months = new[] { "january", "february", "march", "april", "may", "june", "july", "august", "september", "october", "november", "december"}; 

        public ParsedGoalElement(string text) {
            this.text = text.Trim().ToLower().Replace("$", "");
            var yearList = new List<string>();
            for (var i = DateTime.Now.AddYears(-50).Year; i <= DateTime.Now.AddYears(50).Year; i++) {
                yearList.Add(i.ToString());
            }
            years = yearList;
        }

        private void SetNextElement(ParsedGoalElement nextElement) {
            this.NextElement = nextElement;
        }

        public void SetPrevious(ParsedGoalElement previousElement) {
            this.previousElement = previousElement;
            this.previousElement.SetNextElement(this);
        }

        public bool IsADate {
            get {
                DateTime dateTime;
                if (DateTime.TryParse(text, out dateTime)) {
                    return true;
                }

                return months.Contains(text) || years.Contains(text) || wordsMeaningDate.Contains(text, StringComparer.OrdinalIgnoreCase);
            }
        }

        public bool IsADirection {
            get { return wordsMeaningLess.Contains(text, StringComparer.OrdinalIgnoreCase); }
        }

        public bool IsAQuantity {
            get {
                decimal tempDecimal;
                return decimal.TryParse(text, out tempDecimal);
            }
        }

        private int GetDateMagnitude() {
            if (IsAQuantity) {
                return (int)GetQuantity();
            }

            if (text == "next") {
                return 1;
            }
            return 0;
        }

        public DateTime? GetDate() {
            DateTime dateTime;
            if (DateTime.TryParse(text, out dateTime)) {
                return dateTime;
            }

            if (years.Contains(text, StringComparer.OrdinalIgnoreCase)) {
                return new DateTime(int.Parse(text), 12, 31);
            }

            if (months.Contains(text, StringComparer.OrdinalIgnoreCase)) {
                var goalMonth = new GoalMonth(text);
                var projectedDay = new DateTime(DateTime.Now.Year, goalMonth.Month, 1).AddMonths(1).AddDays(-1).Day;
                var projectedYear = DateTime.Now.Year;

                if (NextElement != null && NextElement.IsAQuantity) {
                    projectedDay = (int)NextElement.GetQuantity();

                    if (NextElement.NextElement != null && NextElement.NextElement.IsADate) {
                        projectedYear = NextElement.NextElement.GetDate().Value.Year;
                    }
                }

                var projectedDate = new DateTime(projectedYear, goalMonth.Month, projectedDay);
                return projectedDate;
            }

            if (!wordsMeaningDate.Contains(text, StringComparer.OrdinalIgnoreCase)) return null;

            var yearMagnitude = previousElement.GetDateMagnitude();
            return new DateTime(DateTime.Now.AddYears(yearMagnitude).Year, 12, 31);
        }

        public GoalDirection GetDirection() {
            return wordsMeaningLess.Contains(text, StringComparer.OrdinalIgnoreCase)
                       ? GoalDirection.Descending
                       : GoalDirection.Ascending;
        }

        public decimal GetQuantity() {
            return decimal.Parse(text);
        }        
    }
}