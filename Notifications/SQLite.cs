using Microsoft.EntityFrameworkCore;
using BitcoinBasedNode.Model;

namespace BitcoinBasedNode.Notifications
{
    class SaveDB : DbContext 
    {
        public SaveDB()
        {
            Database.EnsureCreated();
        }
        
        public DbSet<Transaction> _transaction { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder ob)
        {
            ob.UseSqlite(@"Data Source=transactions.db");
            
            
        }



    }
}
