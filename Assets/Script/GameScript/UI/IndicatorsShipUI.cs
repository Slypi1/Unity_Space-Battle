using UnityEngine;
using UnityEngine.UI;

public class IndicatorsShipUI : MonoBehaviour
{
    #region Variables
    [Header("Ship")]
    [SerializeField] private Ship _ship;
    [Header("Slider")]
    [SerializeField] private Slider _helthBar;
    [SerializeField] private Slider _shieldDar;
    #endregion

    private void Awake()
    {
        _helthBar.maxValue = _ship.Life;
        _shieldDar.maxValue = _ship.Shield.ShieldCurrent;
    }

    private void Update()
    {
        _helthBar.value = _ship.Life;
        _shieldDar.value = _ship.Shield.ShieldCurrent;
    }
}
