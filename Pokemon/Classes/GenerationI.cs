using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon.Classes
{
    class GenerationI
    {
        public Species[] pokemon_species { get; set; }
        public type[] types { get; set; }
    }
    public class Species
    {
        public string name { get; set; }
        public string url { get; set; }
    }
}
