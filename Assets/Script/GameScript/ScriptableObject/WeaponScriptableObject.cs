using UnityEngine;

[CreateAssetMenu(fileName = "Weapon",menuName ="Module/New Weapon")]
public class WeaponScriptableObject : ItemScriptableObject
{
    [SerializeField] private int _damage;
    [SerializeField] private int _recharge;
    public int Damage { get { return _damage; } }
    public int Recharge { get { return _recharge; } }
    
}
