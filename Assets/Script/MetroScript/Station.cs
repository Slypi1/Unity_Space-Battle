using System.Collections.Generic;

namespace Metro
{
    public class Station
    {
        public string Name { get; set; }
        public List<string> Line { get; set; }
        public List<Station> Connections { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}


