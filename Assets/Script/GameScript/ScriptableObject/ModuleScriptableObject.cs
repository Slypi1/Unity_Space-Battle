using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Module/New Module")]
public class ModuleScriptableObject  : ItemScriptableObject
{
    [SerializeField] private int _meaning;
    public int Meaning { get { return _meaning; } }
     
}
