using Mango.Services.CouponAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.CouponAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        
        public DbSet<Coupon> Coupons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                CouponId = 1,
                CouponCode = "10PERCENTMANGO",
                DiscountAmount = 10,
                MinAmount = 0
            });
            
            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                CouponId = 2,
                CouponCode = "20PERCENTMANGO",
                DiscountAmount = 20,
                MinAmount = 0
            });
            
            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                CouponId = 3,
                CouponCode = "30PERCENTMANGO",
                DiscountAmount = 30,
                MinAmount = 0
            });
            
            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                CouponId = 4,
                CouponCode = "40PERCENTMANGO",
                DiscountAmount = 40,
                MinAmount = 0
            });
            
            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                CouponId = 5,
                CouponCode = "50PERCENTMANGO",
                DiscountAmount = 50,
                MinAmount = 0
            });
        }   
    }
}
