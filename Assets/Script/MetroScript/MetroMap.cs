using System.Collections.Generic;
using System.Linq;

namespace Metro
{
    [System.Serializable]
    public class MetroMap
    {
        public List<Station> Stations { get; set; }

        public MetroMap() => Stations = new List<Station>();

        public void AddStation(Station station) => Stations.Add(station);

        public void AndConnction(Station station1, Station station2)
        {
            station1.Connections.Add(station2);
            station2.Connections.Add(station1);
        }

        public Station FindStation(string name) { return Stations.FirstOrDefault(s => s.Name == name); }

    }
}
