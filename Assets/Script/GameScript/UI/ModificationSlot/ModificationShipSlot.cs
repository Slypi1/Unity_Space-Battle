using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public  class ModificationShipSlot : MonoBehaviour
{
    #region Variables
    [Header("Additional Information")]
    [SerializeField] private int _idSlotMod;

    private List<GanSlotShip> ganSlots = new List<GanSlotShip>();
    private List<ModuleSlotShip> moduleSlots = new List<ModuleSlotShip>();
    #endregion

    #region Event
    private void OnEnable()
    {
        ConstructorShip.OnSpriteGan += AddModificationGan;
        ConstructorShip.OnSpriteGModule += AddModificationModule;
    }

    private void OnDisable()
    {
        ConstructorShip.OnSpriteGan -= AddModificationGan;
        ConstructorShip.OnSpriteGModule -= AddModificationModule;
    }
    #endregion

    private void Start()
    {       
       FindSlot<GanSlotShip>(ganSlots);
       FindSlot<ModuleSlotShip>(moduleSlots);
    }
    private void FindSlot<U>(List<U> list) where U:Component
    {   
        var childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            var slot = transform.GetChild(i).GetComponent<U>();
            if(slot != null) list.Add(slot);
        }
    }

    private  void AddModificationGan(Sprite sprite,int id)
    {
        if (_idSlotMod == id)
        {
            int freeIndex = ganSlots.FindIndex(slot => slot.GetComponent<Image>().sprite == null);
            if (freeIndex >= 0) ganSlots[freeIndex].AddSprite(sprite);          
        }
    }

    private void AddModificationModule(Sprite sprite, int id)
    {
        if (_idSlotMod == id) 
        {
            int freeIndex = moduleSlots.FindIndex(slot => slot.GetComponent<Image>().sprite == null);
            if (freeIndex >= 0) moduleSlots[freeIndex].AddSprite(sprite);     
        }
    }
}
