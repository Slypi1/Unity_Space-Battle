using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Slot<T> : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler where T : ItemScriptableObject
{
    #region Variables
    [Header("Modification")]
    [SerializeField] private T modification;
    [Header("Additional Information")]
    [SerializeField] private int _idSlot;
    private TMP_Text descroption;
    private bool isActiv;
    #endregion

    public static Action<T, int> OnModification;

    #region Event
    private void OnEnable()
    {
        ConstructorShip.OnIsFull += IsActiv;
    }
    private void OnDisable()
    {
        ConstructorShip.OnIsFull -= IsActiv;
    }
    #endregion

    private void Start()
    {
        gameObject.GetComponent<Image>().sprite = modification.Sprite;
        descroption = transform.GetChild(0).GetComponent<TMP_Text>();
        descroption.text = modification.Name + "\n" + modification.Description;
    }

    public void OnPointerEnter(PointerEventData eventData) => descroption.gameObject.SetActive(true);
    public void OnPointerExit(PointerEventData eventData) => descroption.gameObject.SetActive(false);

    public void OnPointerClick(PointerEventData eventData)
    {
        OnModification(modification, _idSlot);
        ActivSlot();
    }

    private void IsActiv(bool activ) => isActiv = activ;
    private void ActivSlot() => gameObject.SetActive(isActiv);
}
