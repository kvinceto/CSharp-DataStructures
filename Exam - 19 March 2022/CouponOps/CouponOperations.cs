using System.Linq;
using System.Security;

namespace CouponOps
{
    using System;
    using System.Collections.Generic;
    using CouponOps.Models;
    using Interfaces;

    public class CouponOperations : ICouponOperations
    {
        private Dictionary<string, Website> websitesByDomain;
        private Dictionary<string, Coupon> couponsByCode;

        public CouponOperations()
        {
            this.websitesByDomain = new Dictionary<string, Website>();
            this.couponsByCode = new Dictionary<string, Coupon>();
        }

        public void AddCoupon(Website website, Coupon coupon)
        {
            if (!this.Exist(website))
            {
                throw new ArgumentException();
            }

            this.couponsByCode.Add(coupon.Code, coupon);
            website.Coupons.Add(coupon.Code, coupon);
            coupon.Sites.Add(website.Domain, website);
        }

        public bool Exist(Website website)
            => this.websitesByDomain.ContainsKey(website.Domain);

        public bool Exist(Coupon coupon)
        => this.couponsByCode.ContainsKey(coupon.Code);

        public IEnumerable<Coupon> GetCouponsForWebsite(Website website)
        {
            if (!this.Exist(website))
                throw new ArgumentException();
            return website.Coupons.Values;
        }

        public IEnumerable<Coupon> GetCouponsOrderedByValidityDescAndDiscountPercentageDesc()
            => this.couponsByCode.Values
                .OrderByDescending(c => c.Validity)
                .ThenByDescending(c => c.DiscountPercentage);

        public IEnumerable<Website> GetSites()
        => this.websitesByDomain.Values;

        public IEnumerable<Website> GetWebsitesOrderedByUserCountAndCouponsCountDesc()
            => this.websitesByDomain.Values
                .OrderBy(w => w.UsersCount)
                .ThenByDescending(w => w.Coupons.Count);

        public void RegisterSite(Website website)
        {
            if (this.websitesByDomain.ContainsKey(website.Domain))
            {
                throw new ArgumentException();
            }
            this.websitesByDomain.Add(website.Domain, website);
        }

        public Coupon RemoveCoupon(string code)
        {
            if (!this.couponsByCode.ContainsKey(code))
            {
                throw new ArgumentException();
            }
            var result = this.couponsByCode[code];
            this.couponsByCode.Remove(code);
            List<string> sites = new List<string>();
            foreach (var kvp in result.Sites)
            {
                sites.Add(kvp.Key);
                kvp.Value.Coupons.Remove(code);
            }

            foreach (var site in sites)
            {
                result.Sites.Remove(site);
            }

            return result;
        }

        public Website RemoveWebsite(string domain)
        {
            if (!this.websitesByDomain.ContainsKey(domain))
            {
                throw new ArgumentException();
            }
            var result = this.websitesByDomain[domain];
            this.websitesByDomain.Remove(domain);
            foreach (var kvp in result.Coupons)
            {
                this.couponsByCode.Remove(kvp.Key);
            }

            return result;
        }

        public void UseCoupon(Website website, Coupon coupon)
        {
            if (!this.Exist(website) || !website.Coupons.ContainsKey(coupon.Code))
                throw new ArgumentException();
            website.Coupons.Remove(coupon.Code);
            this.couponsByCode.Remove(coupon.Code);
            coupon.Sites.Remove(website.Domain);
        }
    }
}
