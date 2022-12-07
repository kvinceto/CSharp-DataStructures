using System.Collections.Generic;

namespace CouponOps.Models
{
    public class Website
    {
        public Website(string domain, int usersCount)
        {
            this.Domain = domain;
            this.UsersCount = usersCount;
            this.Coupons = new Dictionary<string, Coupon>();
        }

        public string Domain { get; set; }
        public int UsersCount { get; set; }
        public Dictionary<string, Coupon> Coupons { get; set; }

        public override bool Equals(object obj)
        {
            return this.Domain == ((Website)obj).Domain;
        }
    }
}
