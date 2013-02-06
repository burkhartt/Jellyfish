namespace Domain.Models.Goals {
    public class QuantitativeGoal : Goal {
        public QuantitativeGoal() {
            CurrentNumber = 0;
            TargetNumber = 0;
        }

        public decimal CurrentNumber { get; set; }
        public decimal TargetNumber { get; set; }
    }
}