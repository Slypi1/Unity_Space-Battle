using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Metro;
public class MetroMapManager : MonoBehaviour
{
    MetroMap metroMap = new MetroMap();
   
    #region Event
    private void OnEnable()
    {
        UIManager.OnStation += AddStation;
        UIManager.OnConnected += Connected;        
    }
    
    private void OnDisable()
    {
        UIManager.OnStation -= AddStation;
        UIManager.OnConnected -= Connected;
    }
    #endregion

    private void AddStation(string name, List<string> lineName)
    {
        Station station = new Station() { Name = name, Line = lineName, Connections = new List<Station>() };
        metroMap.AddStation(station);
        SaveMetroMap();
    }

    private void Connected(string nameOne, string nameTwo)
    {
        var station1 = metroMap.FindStation(nameOne);
        var station2 = metroMap.FindStation(nameTwo);
        metroMap.AndConnction(station1, station2);
        SaveMetroMap();
    }
    
    private void SaveMetroMap()
    {
        string filePath = Application.dataPath + "/data.json";
        var settings = new JsonSerializerSettings
        { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
        var json = JsonConvert.SerializeObject(metroMap, Formatting.Indented, settings);
        File.WriteAllText(filePath, json);
    }

    public  MetroMap LoadMetroMap()
    {
        string filePath = Application.dataPath + "/data.json";
        var json = File.ReadAllText(filePath);      
        var metro = JsonConvert.DeserializeObject<MetroMap>(json);
        return metro;
    }
}
