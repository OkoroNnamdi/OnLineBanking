using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnLineBanking.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Core
{
    public  class OnlineBankDBContext: IdentityDbContext<AppUser>
    {
        public OnlineBankDBContext(DbContextOptions <OnlineBankDBContext> options):base(options)
        {
            
        }
        public DbSet<Account>accounts { get; set; }
        public DbSet<Address> addresses { get; set; }
        public DbSet<BankBranch> BankBranches { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<ManagerRequest> ManagerRequests { get;set; }
        public DbSet<Payment>Payments { get; set; }
        public DbSet <Rating> Ratings { get; set; }
        public DbSet <Review > Reviews { get; set; }
        public DbSet <State> State { get; set; }
        public DbSet <Transaction > Transaction { get; set; }
        public DbSet <User > users { get; set; }
        public DbSet <WishList > WishList { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var item in ChangeTracker.Entries<BaseEntity>())
            {
                switch (item.State)
                {
                    case EntityState.Modified:
                        item.Entity.UpdatedAt = DateTime.UtcNow;
                        break;
                    case EntityState.Deleted:
                        item.Entity.IsDeleted = true;
                        break;
                    case EntityState.Added:
                        item.Entity.Id = Guid.NewGuid().ToString();
                        item.Entity.CreatedAt = DateTime.UtcNow;
                        break;
                    default:
                        break;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
