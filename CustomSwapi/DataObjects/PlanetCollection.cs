using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomSwapi.DataObjects
{
    public class PlanetCollection
    {
        public int count { get; set; }
        public string next { get; set; }
        public string previous { get; set; }
        public List<Planet> results { get; set; }
    }
}
