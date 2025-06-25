using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RedGari.Domain.Entities;

namespace RedGari.Infrastructure.Data
{
    public class CharacterDbContext : DbContext
    {
        public CharacterDbContext(DbContextOptions<CharacterDbContext> options) : base(options) { }
        public DbSet<DaggerHeart_Character> Characters => Set<DaggerHeart_Character>();
    }
}
