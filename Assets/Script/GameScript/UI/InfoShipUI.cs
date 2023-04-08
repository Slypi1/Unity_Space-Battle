using UnityEngine;
using TMPro;

public class InfoShipUI : MonoBehaviour
{
    [Header("Ship")]
    [SerializeField] private Ship _ship;
    [Header("Text")]
    [SerializeField] private TMP_Text _life;
    [SerializeField] private TMP_Text _shield;
    [SerializeField] private TMP_Text _damage;

    private void Awake() => GetShipData();
    private void Update() => GetShipData();
  
    private void GetShipData()
    {
        _life.text = _ship.Life.ToString();
        _shield.text = _ship.ShieldLife.ToString();
        _damage.text = _ship.Damage.ToString();
    }
}
