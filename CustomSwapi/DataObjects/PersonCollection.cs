using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomSwapi.DataObjects
{
    public class PersonCollection
    {
        public int count { get; set; }
        public string next { get; set; }
        public List<Person> results { get; set; }
    }
}
