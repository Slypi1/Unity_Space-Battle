using UnityEngine;

public class Weapon : MonoBehaviour
{
    #region Variables
    [Header("Bullet")]
    [SerializeField] private Bullet _bullet;
    [Header("Effect")]
    [SerializeField] private ParticleSystem _free;

    private int _recgarge;
    private float lastFireTime = -Mathf.Infinity;
    #endregion
    public WeaponScriptableObject slot { get; set; }
    public Bullet Bullet { get { return _bullet; } }

    public void AddValurRecgarge(int value) => _recgarge -= _recgarge * value / 100; 

    public void Shot()
    {
        GetRecgarge();
        if (Time.time > lastFireTime + _recgarge)
        { 
            CreationBullet();
            lastFireTime = Time.time;
        }                               
    }

    private void GetRecgarge() => _recgarge = slot.Recharge;

    private void CreationBullet()
    {            
        Instantiate(_bullet, transform.position, transform.rotation, transform);
        _bullet.AddDamage(slot.Damage);
        Instantiate(_free, transform.position, transform.rotation, transform);
    }
}
