using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EnsekAPI;

namespace EnsekAPI.Data
{
    public class EnsekAPIContext : DbContext
    {
        public EnsekAPIContext (DbContextOptions<EnsekAPIContext> options)
            : base(options)
        {
        }

        public DbSet<EnsekAPI.MeterReading> MeterReading { get; set; } = default!;
    }
}
