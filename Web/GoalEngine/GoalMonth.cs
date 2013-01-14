namespace GoalEngine {
    public class GoalMonth {
        private readonly string month;

        public GoalMonth(string month) {
            this.month = month;
        }

        public int Month {
            get {
                switch (month.ToLower().Trim()) {
                    case "january":
                        return 1;
                    case "february":
                        return 2;
                    case "march":
                        return 3;
                    case "april":
                        return 4;
                    case "may":
                        return 5;
                    case "june":
                        return 6;
                    case "july":
                        return 7;
                    case "august":
                        return 8;
                    case "september":
                        return 9;
                    case "october":
                        return 10;
                    case "november":
                        return 11;
                    case "december":
                        return 12;
                }
                return 12;
            }
        }
    }
}