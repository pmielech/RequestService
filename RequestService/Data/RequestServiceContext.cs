using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RequestService.Data
{
    public class RequestServiceContext : DbContext
    {
        public RequestServiceContext (DbContextOptions<RequestServiceContext> options)
            : base(options)
        {
        }

        public DbSet<RequestService.Data.GymMembershipType> GymMembershipTypes { get; set; } = default!;
    }
}
