using System;
using NUnit.Framework;

namespace GoalEngine.Tests {
    [TestFixture]
    public class GoalParserTests {
        private ParsedGoal Parse(string goal) {
            var goalParser = new GoalParser();
            return goalParser.Parse(goal);
        }        

        [Test]
        public void It_should_return_the_correct_data() {
            var result = Parse("find the most peaceful place on Earth");
            result.ShouldEqual(null, GoalDirection.None, null);            
        }

        [Test]
        public void It_should_return_the_correct_data_2() {
            var result = Parse("read 12 books in 2010");
            result.ShouldEqual(12, GoalDirection.Ascending, new DateTime(2010, 12, 31));            
        }

        [Test]
        public void It_should_return_the_correct_data_3() {
            var result = Parse("visit all 50 states");
            result.ShouldEqual(50, GoalDirection.Ascending, null);            
        }

        [Test]
        public void It_should_return_the_correct_data_4() {
            var result = Parse("indigostrings wants to run 10 miles");
            result.ShouldEqual(10, GoalDirection.Ascending, null);            
        }

        [Test]
        public void It_should_return_the_correct_data_5() {
            var result = Parse("Visit 50 of the world heritage sites");
            result.ShouldEqual(50, GoalDirection.Ascending, null);            
        }

        [Test]
        public void It_should_return_the_correct_data_6() {
            var result = Parse("lose 20 lbs");
            result.ShouldEqual(20, GoalDirection.Descending, null);
        }

        [Test]
        public void It_should_return_the_correct_data_7() {
            var result = Parse("createkg wants to Lose 30 pounds");
            result.ShouldEqual(30, GoalDirection.Descending, null);
        }

        [Test]
        public void It_should_return_the_correct_data_8() {
            var result = Parse("Get 100 followers on tumblr.");
            result.ShouldEqual(100, GoalDirection.Ascending, null);
        }

        [Test]
        public void It_should_return_the_correct_data_9() {
            var result = Parse("Lose 30 pounds");
            result.ShouldEqual(30, GoalDirection.Descending, null);
        }

        [Test]
        public void It_should_return_the_correct_data_10() {
            var result = Parse("run a half marathon in under 2 hrs");
            result.ShouldEqual(2, GoalDirection.Descending, null);
        }

        [Test]
        public void It_should_return_the_correct_data_11() {
            var result = Parse("to save $1000 by 12/1/2013");
            result.ShouldEqual(1000, GoalDirection.Ascending, new DateTime(2013, 12, 1));
        }

        [Test]
        public void It_should_return_the_correct_data_12() {
            var result = Parse("to get less than a 70 in golf this year");
            result.ShouldEqual(70, GoalDirection.Descending, new DateTime(DateTime.Now.Year, 12, 31));
        }

        [Test]
        public void It_should_return_the_correct_data_13() {
            var result = Parse("to have $1000000 in the next 13 years");
            result.ShouldEqual(1000000, GoalDirection.Ascending, new DateTime(DateTime.Now.AddYears(13).Year, 12, 31));
        }
        
        [Test]
        public void It_should_return_the_correct_data_14() {
            var result = Parse("to find a job that pays at least $500000");
            result.ShouldEqual(500000, GoalDirection.Ascending, null);
        }

        [Test]
        public void It_should_return_the_correct_data_15() {
            var result = Parse("to gain 15 lbs by November 15");
            result.ShouldEqual(15, GoalDirection.Ascending, new DateTime(DateTime.Now.Year, 11, 15));
        }

        [Test]
        public void It_should_return_the_correct_data_16() {
            var result = Parse("I want to go to the last 5 states in the United States by December 18, 2029");
            result.ShouldEqual(5, GoalDirection.Ascending, new DateTime(2029, 12, 18));
        }

        [Test]
        public void It_should_return_the_correct_data_17() {
            var result = Parse("I want to meet every president next year");
            result.ShouldEqual(null, GoalDirection.None, new DateTime(DateTime.Now.AddYears(1).Year, 12, 31));
        }
    }
}