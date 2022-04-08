using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace KaasService.Models
{
    public class KaaslandContext:DbContext
    {
        public KaaslandContext(DbContextOptions<KaaslandContext> options) : base(options) { }
        public DbSet<Kaas> Kazen { get; set; }
    }
}
