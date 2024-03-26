using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SoftITOFlix.Models;

namespace SoftITOFlix.Data
{
    public class SoftITOFlixContext : DbContext
    {
        public SoftITOFlixContext (DbContextOptions<SoftITOFlixContext> options)
            : base(options)
        {
        }

        public DbSet<SoftITOFlix.Models.Category> Categories { get; set; } = default!;
    }
}
