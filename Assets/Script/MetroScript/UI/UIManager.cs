using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using Metro;
public class UIManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private TMP_InputField _stationName;
    [SerializeField] private TMP_InputField _connectedStations;
    [SerializeField] private TMP_InputField _lines;
    [SerializeField] private TMP_InputField _findPath;

    [SerializeField] private TMP_Text _path;
    [SerializeField] private TMP_Text _transfers;
    #endregion

    public static Action<string,List<string>> OnStation;
    public static Action<string,string> OnConnected;
    public static Action<string, string> OnStations;

    #region Event
    public void OnEnable()
    {
       Route.OnPath += InputPath;
       Route.OnTransfers += InputTransfers;
    }

    public void OnDisable()
    {
        Route.OnPath -= InputPath;
        Route.OnTransfers -= InputTransfers;
    }
    #endregion

    public void OnAddStation()
    {
        string name = _stationName.text;
        string[] lines= _lines.text.Split(',');
        OnStation(name, GetText(lines));
        _stationName.text = "";
       _lines.text = "";
    }

    public void OnAddConnected()
    {
        string[] connected = _connectedStations.text.Split(',');
        OnConnected(connected[0], connected[1]);
        _connectedStations .text = "";
    }

    public void OnFindPath()
    {
        string[] stations = _findPath.text.Split(',');
        OnStations(stations[0], stations[1]);
    }

    private void InputTransfers(int transfers)
    {
        _transfers.text = "";
        _transfers.text = transfers.ToString();
    }

    private void InputPath(List<Station> path)
    {
        _path.text = "";
        if (path != null) 
        { 
            List<string> stations = GetStations(path);
            InputStations(stations);
        }      
    }

    private void InputStations(List <string> namesSt)
    {
        foreach (string nameSt in namesSt) { _path.text += nameSt; }                 
    }

    private List <string> GetStations(List<Station> path)
    {
        List<string> namesSt = new List<string>();
        foreach (Station station in path) { namesSt.Add(station.Name); }               
        return namesSt;
    }

    private List<string> GetText(string[] linesSt)
    {
        List<string> lines = new List<string>();
        foreach (var line in linesSt) { lines.Add(line); }    
        return lines;
    }
}
