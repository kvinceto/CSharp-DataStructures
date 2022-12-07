using System.Collections.Generic;

namespace CouponOps.Models
{
    public class Coupon
    {
        public Coupon(string code, int discountPercentage, int validity)
        {
            this.Code = code;
            this.DiscountPercentage = discountPercentage;
            this.Validity = validity;
            this.Sites = new Dictionary<string, Website>();
        }

        public string Code { get; set; }
        public int DiscountPercentage { get; set; }
        public int Validity { get; set; }
        public Dictionary<string, Website> Sites { get; set; }

        public override bool Equals(object obj)
        {
            return this.Code == ((Coupon)obj).Code;
        }
    }
}
