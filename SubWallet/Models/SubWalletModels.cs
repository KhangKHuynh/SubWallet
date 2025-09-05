
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
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public  DateTime StartDate { get; set; }
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
                        return StartDate; // not used but will be added in the future 
                }

            }
            set { _nextDate = value; }
        }

        public void ResetNextDate()
        {
            _nextDate = null; //not used but for future project Ideas 
        }
    }
}