using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Data.Context
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseMySql("Persist Security Info=False;" +
                "Username=afnpctqwttgxxj85;" +
                "Password=ojs6ebsxq2nh8ftv;" +
                "database=sht40aklp29l6ppo;" +
                "server=n2o93bb1bwmn0zle.chr7pe7iynqr.eu-west-1.rds.amazonaws.com;" +
                "Connect Timeout=3600;" +
                "SslMode=Required");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
