using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    #region Variables
    [Header("Ship Characteristics")]
    [SerializeField] private int _life;
    [SerializeField] private int _shieldLife;
    [SerializeField] private int _moduleCount;
    [Header("Components")]
    [SerializeField] protected ModuleScriptableObject[] _moduleSlots;
    [SerializeField] protected List<Weapon> _weapon;
    [Header("Additional Information")]
    [SerializeField] protected int _idShip;
    [SerializeField] private Sprite _spiteShip;
    
    private Shield _shield;  
    private int _damage;
    #endregion

    #region
    public int Life { get { return _life; }}
    public int ShieldLife { get { return _shieldLife; } }
    public int Damage { get { return _damage; } }
    public Shield Shield { get { return _shield;} }
    public List <Weapon> Weapon { get { return _weapon; } }
    public Sprite SpiteShip { get { return _spiteShip; } }
    #endregion

    private void Awake()
    {
        ConnectionComponents();
        ConnectionModule();
        CounShield();       
    }
    private void Update()
    {
        Destroyed();
        ControlShield();
    }

    /// <summary>
    /// не унивирсальный метод, подходит только к иерархии моделей как в проекте
    /// </summary>
    private void ConnectionComponents()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<Weapon>()) _weapon.Add(transform.GetChild(i).GetComponent<Weapon>());
            else if (transform.GetChild(i).GetComponent<Shield>()) _shield = transform.GetChild(i).GetComponent<Shield>();
        }
    }
    
    private void ConnectionModule() => _moduleSlots = new ModuleScriptableObject[_moduleCount];
    protected void AddDamage(int damage) => _damage += damage;
    private void CounShield() => _shield.AddValueShield(ShieldLife);

    private void CounRecgarhe(int value)
    {
        foreach (var g in _weapon)
        {
            g.AddValurRecgarge(value);
        }
    }
    
    protected void UseModule(ModuleScriptableObject module)
    {
        switch (module.name)
        {
            case "Модуль А": _shieldLife += module.Meaning; CounShield(); break;
            case "Модуль Б": _life += module.Meaning; break;
            case "Модуль С": CounRecgarhe(module.Meaning); break;
            case "Модуль Д": _shield.AddValueShieldRate(module.Meaning); break;
        }
    }

    private void Destroyed()
    {
        if (_life <= 0) gameObject.SetActive(false);
    }

    public void Fire(Transform target)
    {
        foreach (var gan in _weapon)
        {
            gan.Shot();
            gan.Bullet.Target = target;
        }
    }
   
    public void TakeDamage(int damage)
    {
        if (_shield.IsBroken) _life -= damage;               
    }

    private void ControlShield()
    {
        if(_shield.IsBroken)
        _shield.Reconscruction();
    } 
}
