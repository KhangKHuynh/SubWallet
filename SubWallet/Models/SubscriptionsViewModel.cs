using System.Collections.Generic;

namespace SubWallet.Models
{
    public class SubscriptionsViewModel
    {
        public List<Subscription> Subscriptions { get; set; } = new();
        public Subscription NewSubscription { get; set; } = new Subscription();
    }
}




