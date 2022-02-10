using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomSwapi.DataObjects
{
    public class Person
    {
        public string id { get; set; }
        public string name { get; set; }
        public string birth_year { get; set; }
        public string gender { get; set; }
        public string url { get; set; }
    }
}
