using System;
using UnityEngine;

public class ConstructorShip : Ship
{
    private bool isFull;

    public static Action<Sprite, int> OnSpriteGan;
    public static Action<Sprite, int> OnSpriteGModule;   
    public static Action<bool> OnIsFull;

    #region Event
    private void OnEnable()
    {
        Slot<WeaponScriptableObject>.OnModification += WeaponMod;
        Slot<ModuleScriptableObject>.OnModification += ModuleMod;
    }

    private void OnDisable()
    {
        Slot<WeaponScriptableObject>.OnModification -= WeaponMod;
        Slot<ModuleScriptableObject>.OnModification -= ModuleMod;
    }
    #endregion

    private void WeaponMod(WeaponScriptableObject weapon, int id)
    {
        if (_idShip == id) ConstructWeapon(weapon);       
    }
    private void ConstructWeapon(WeaponScriptableObject weapon)
    {
        int freeIndex = _weapon.FindIndex(_weapon => _weapon.slot  == null);
        if (freeIndex >= 0)
        {
            _weapon[freeIndex].slot = weapon;
            OnSpriteGan(weapon.Sprite, _idShip);
            Full(isFull = false);
            return;
        }
        else
        {
            isFull = true;
            Full(isFull);
        }
    }

    private void ModuleMod(ModuleScriptableObject module, int id)
    {
        if (_idShip == id) AddModule(module);    
    }

    private void AddModule(ModuleScriptableObject module)
    {
        for (int i = 0; i < _moduleSlots.Length; i++)
        {
            if (_moduleSlots[i] == null)
            {
                _moduleSlots[i] = module;
                UseModule(module);
                OnSpriteGModule(module.Sprite, _idShip);
                Full(isFull = false);
                return;
            }
        }
        isFull = true;
        Full(isFull);
    }

    private void Full(bool isFull) => OnIsFull(isFull);
}
