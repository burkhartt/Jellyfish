using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Cliver;

namespace GoalEngine {
    public class GoalParser {
        public ParsedGoal Parse(string goal) {
            goal = goal.ToLower().Trim();
            DateTime? deadline = null;
            decimal? quantity = null;
            var goalDirection = GoalDirection.None;

            var numbers = Regex.Split(goal, "[^\\d]").Where(x => !string.IsNullOrEmpty(x)).Select(decimal.Parse);
            var foundANumber = false;

            DateTime parsedDeadline;
            if (goal.TryParseDate(DateTimeRoutines.DateTimeFormat.USA_DATE, out parsedDeadline)) {
                deadline = parsedDeadline;
            }

            if (TheGoalContainsAPhraseIndicatingAYear(goal)) {
                deadline = GetYearFromText(goal);
            }

            foreach (var number in numbers) {
                if (deadline == null && NumberIsYear((int) number)) {
                    deadline = new DateTime((int) number, 12, 31);
                    continue;
                }

                if (foundANumber) continue;

                quantity = number;
                goalDirection = GetMostLikelyDirection(goal);
                foundANumber = true;
            }

            return new ParsedGoal {
                Deadline = deadline,
                Quantity = quantity,
                Direction = goalDirection
            };
        }

        private DateTime? GetYearFromText(string goal) {
            if (goal.Contains("this year")) {
                return new DateTime(DateTime.Now.Year, 12, 31);
            }

            var numberOfYearsRegex = new Regex(@"\d+ years");
            if (numberOfYearsRegex.IsMatch(goal)) {                
                var numberOfYearsValue = numberOfYearsRegex.Match(goal).Value;
                var numberOfYears = new Regex(@"\d+");
                var years = int.Parse(numberOfYears.Match(numberOfYearsValue).Value);
                return new DateTime(DateTime.Now.AddYears(years).Year, 12, 31);
            }
            return null;
        }

        private bool TheGoalContainsAPhraseIndicatingAYear(string goal) {
            return GetYearFromText(goal) != null;
        }

        private GoalDirection GetMostLikelyDirection(string goal) {
            return WordsOrPhrasesThatMeanDescending().Any(goal.Contains) ? GoalDirection.Descending : GoalDirection.Ascending;
        }

        private IEnumerable<string> WordsOrPhrasesThatMeanDescending() {
            return new[] {
                "lose",
                "under",
                "less than"
            };
        }

        private bool NumberIsYear(int number) {
            var startYear = DateTime.Now.AddYears(-50).Year;
            var endYear = DateTime.Now.AddYears(100).Year;
            return number >= startYear && number <= endYear;
        }
    }
}