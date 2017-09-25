using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManager
{
    class JsonSocketObjects
    {
        public class Sensor
        {
            public string action { get; set; }
            public int id { get; set; }
            public string name { get; set; }
            public string statusOnOff { get; set; }
            public string statusStartStop { get; set; }
            public string type { get; set; }
            public string[] sensors { get; set; }
        }
    }
}
