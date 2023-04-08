using UnityEngine;

public enum ModType {Weapon,Module }
public class ItemScriptableObject : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private Sprite _sprite;
    
    public string Name { get { return _name;}} 
    public string Description { get { return _description;} }
    public Sprite Sprite { get { return _sprite; } }

}


