
namespace SubWallet.Models
{
    public enum BillingCycle
    {
        Weekly,
        Monthly,
        Yearly,
        BiWeekly,
    }
    public class Subscription
    {
        public required int ID { get; set; }
        public required string Name { get; set; }
        public required decimal Cost { get; set; }
        public required DateTime StartDate { get; set; }
        public BillingCycle Cycle { get; set; }
        
        private DateTime? _nextDate;

        public DateTime NextDate
        {
            get
            {
                if (_nextDate.HasValue)
                {
                    return _nextDate.Value;
                }

                switch (Cycle)
                {
                    case BillingCycle.Weekly:
                        return StartDate.AddDays(7);
                    case BillingCycle.BiWeekly:
                        return StartDate.AddDays(14);
                    case BillingCycle.Monthly:
                        return StartDate.AddDays(30);
                    case BillingCycle.Yearly:
                        return StartDate.AddDays(365);
                    default:
                        return StartDate;
                }
                
            }
            set
            {
                _nextDate = value;
            }
        }

        public void ResetNextDate()
        {
            _nextDate = null;
        }
    }
}