using System;
using System.Collections.Generic;
using System.Linq;
using Metro;


public class Route : MetroMapManager
{
    MetroMap metroMap = new MetroMap();

    public static Action<List<Station>> OnPath;
    public static Action<int> OnTransfers;

    private void Awake()
    {
        metroMap =  LoadMetroMap();
    }
    #region Event
    private void OnEnable()
    {
        UIManager.OnStations += FindPath;
    }

    private void OnDisable()
    {
        UIManager.OnStations -= FindPath;
    }
    #endregion

    private void FindPath(string nameOne, string nameTwo)
    {
        List<Station> stations = FindShortestPath(nameOne, nameTwo);
        OnPath(stations);
        FindTransfers(stations);  
    }

    private void FindTransfers(List<Station> stations)
    {
        int count = FindTransfersPath(stations);
        OnTransfers(count);
    }

    private List<Station> FindShortestPath(string startStationName, string endStationName)
    {
        var startStation = metroMap.FindStation(startStationName);
        var endStation = metroMap.FindStation(endStationName);
        var visited = new HashSet<Station>();
        var queue = new Queue<List<Station>>();
        queue.Enqueue(new List<Station> { startStation });

        while (queue.Count > 0)
        {
            var path = queue.Dequeue();
            var lastStation = path.Last();

            if (!visited.Contains(lastStation))
            {
                visited.Add(lastStation);
                if (lastStation.Name == endStation.Name) { return path; }
                foreach (Station neighbour in lastStation.Connections)
                {

                    var newPath = new List<Station>(path) { neighbour };
                    queue.Enqueue(newPath);
                }
            }
        }
        return null;
    }

    private int FindTransfersPath(List<Station> stations)
    {
        int transfers = 0;
        for (int i = 0; i < stations.Count - 2; i++)
        {
            Station prevStation = stations[i];
            Station midleStation = stations[i + 1];
            Station endStation= stations[i + 2];
            foreach (string line in prevStation.Line.Intersect(midleStation.Line))
            {
                if (!endStation.Line.Contains(line))
                {
                    transfers++;
                    break;
                }
            }
        }
        return transfers;
    }
}
