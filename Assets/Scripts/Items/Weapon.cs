using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "weapon")]
public class Weapon : ScriptableObject
{
    public string weaponName;
    public string animName;
    public string discription;
    public WeaponType type;
    public int id;
    
    public int cost;
    public Sprite sprite;
    public float fireRate;
    public int magSize;
    public float reloadSpeed;
    public GameObject projectile;


    public static Weapon FindWeapon(int id)
    {
        foreach (Weapon weapon in GameData.instance.allWeapons)
        {
            if (weapon.id == id)
                return weapon;
        }
        Debug.LogError("Unable to find weapon with id: " + id.ToString());
        return null;
    }

    /*      WEAPON IDs
     * -----------------
     * Unarmed: 100
     * Revolver: 101
     * Shotgun: 102
     * Uzi 103
     */

}

public enum WeaponType
{
    Unarmed, Handgun, Shotgun, Uzi
}