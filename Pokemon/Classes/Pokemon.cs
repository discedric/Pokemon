using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon.Classes
{
    public class pokemon
    {
        public int id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public int height { get; set; }
        public int weight { get; set; }
        public stats[] stats { get; set; }
        public types[] types { get; set; }
        public int base_experience { get; set; }
        public sprites sprites { get; set; }
    }
    public class sprites
    {
        public string back_default { get; set; }
        public string back_shiny { get; set; }
        public string front_default { get; set; }
        public string front_shiny { get; set; }
    }
    public class types
    {
        public int slot { get; set; }
        public type type { get; set; }
    }
    public class type
    {
        public string name { get; set; }
        public string url { get; set; }
    }
    public class stats
    {
        public int base_stat { get; set; }
        public int effort { get; set; }
        public stat stat { get; set; }
    }
    public class stat
    {
        public string name { get; set; }
        public string url { get; set; }
    }
}
