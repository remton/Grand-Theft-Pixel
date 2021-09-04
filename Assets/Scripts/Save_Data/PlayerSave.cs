using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerSave
{
    public int activeCarID;
    public int[] ownedCarIDs;

    public int activeWeaponID;
    public int[] ownedWeaponIDs;

    public int bank;

    public PlayerSave(PlayerData data)
    {
        activeCarID = data.activeCar.id;
        activeWeaponID = data.activeWeapon.id;

        ownedCarIDs = new int[data.ownedCars.Count];
        for (int i = 0; i < data.ownedCars.Count; i++)
        {
            ownedCarIDs[i] = data.ownedCars[i].id;
        }
        ownedWeaponIDs = new int[data.ownedWeapons.Count];
        for (int i = 0; i < data.ownedWeapons.Count; i++)
        {
            ownedWeaponIDs[i] = data.ownedWeapons[i].id;
        }

        bank = data.bank;
    }
}
