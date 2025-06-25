using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedGari.Domain.Entities
{
    public class DaggerHeart_Character
    {
        public long Id { get; set; }
        public string Name { get; set; } = "Unknown";
        public int Heritage { get; set; }
        public int Class { get; set; }
        public int Subclass { get; set; }
        public int Level { get; set; } = 1;
    }
}
