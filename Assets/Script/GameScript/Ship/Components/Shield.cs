using UnityEngine;

public class Shield : MonoBehaviour
{
    #region Variables
    [Header("Characteristics")]
    [SerializeField] private float _shieldRate;
    [Header("Effect")]
    [SerializeField] private ParticleSystem _particleSystem;

    private int _shieldMax;
    private float _shieldCurrent;
    private bool _isBroken;
    #endregion
    public bool IsBroken { get { return _isBroken; } }
    public float ShieldCurrent { get { return _shieldCurrent;}}

    private void Update() => Broken();
    private void Broken()
    {
        if (_shieldCurrent <= 0)
        {
            _isBroken = true;
            gameObject.SetActive(false);
        }
    }

    public void Reconscruction()
    {
        if (_isBroken)
        {       
            _shieldCurrent += _shieldRate * Time.deltaTime;
            _shieldCurrent = Mathf.Clamp(_shieldCurrent, 0, _shieldMax);
            if (_shieldCurrent == _shieldMax)
            {
                gameObject.SetActive(true);
                _isBroken = false;
            }
        }      
    }

    public void AddValueShield(int value) => _shieldCurrent = _shieldMax = value;    
    public void AddValueShieldRate(int value) => _shieldRate += _shieldRate * value / 100;
    public void TakeDamage(int damage) => _shieldCurrent -= damage;
}
