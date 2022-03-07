using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedLibrary;

namespace APIDemoLFT.DataAccess
{
    public class LFTDatabaseContext: DbContext
    {
        public LFTDatabaseContext(DbContextOptions options) : base(options) { }        
        public DbSet<Application> Applications { get; set; }        
        public DbSet<Client> Clients { get; set; }
        public DbSet<Business> Businesses { get; set; }
        public DbSet<Loan> Loans { get; set; }
    }
}
