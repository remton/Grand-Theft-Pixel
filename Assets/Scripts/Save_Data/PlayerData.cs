using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData instance;

    [Header("Data loaded at Runtime")]

    public Car activeCar;
    public Weapon activeWeapon;
    public List<Car> ownedCars;
    public List<Weapon> ownedWeapons;
    public int bank;

    public void Awake()
    {
        if(instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
            Load();
        }
    }

    public static void Save()
    {
        Debug.Log("Saving game");
        SaveSystem.Save(instance);
    }

    public static void Load()
    {
        PlayerSave save = SaveSystem.LoadSave();
        if (save == null)
        {
            ClearData();
            Save();
            return;
        }
        
        instance.activeCar = Car.FindCar(save.activeCarID);
        instance.activeWeapon = Weapon.FindWeapon(save.activeWeaponID);

        instance.ownedCars.Clear();
        for (int i = 0; i < save.ownedCarIDs.Length; i++)
        {
            instance.ownedCars.Add(Car.FindCar(save.ownedCarIDs[i]));
        }
        instance.ownedWeapons.Clear();
        for (int i = 0; i < save.ownedWeaponIDs.Length; i++)
        {
            instance.ownedWeapons.Add(Weapon.FindWeapon(save.ownedWeaponIDs[i]));
        }

        instance.bank = save.bank;
    }

    public void ClearDataWrapper()
    {
        ClearData();
    }
    public static void ClearData()
    {
        instance.ownedCars = new List<Car>();
        instance.ownedCars.Add(GameData.instance.startCar);
        instance.activeCar = GameData.instance.startCar;

        instance.ownedWeapons = new List<Weapon>();
        instance.ownedWeapons.Add(GameData.instance.startWeapon);
        instance.activeWeapon = GameData.instance.startWeapon;

        instance.bank = 0;
        Save();
        Debug.Log("PLAYER DATA CLEARED");
    }

    public static bool IsOwned(Car car)
    {
        for (int i = 0; i < instance.ownedCars.Count; i++)
        {
            if (instance.ownedCars[i] == car)
                return true;
        }
        return false;
    }
    public static bool IsOwned(Weapon weapon)
    {
        for (int i = 0; i < instance.ownedWeapons.Count; i++)
        {
            if (instance.ownedWeapons[i] == weapon)
                return true;
        }
        return false;
    }

    public static List<Car> GetUnownedCars()
    {
        List<Car> unowned = new List<Car>();
        foreach (Car car in GameData.instance.allCars)
        {
            if (!IsOwned(car))
                unowned.Add(car);
        }
        return unowned;
    }
    public static List<Weapon> GetUnownedWeapons()
    {
        List<Weapon> unowned = new List<Weapon>();
        foreach (Weapon weap in GameData.instance.allWeapons)
        {
            if (!IsOwned(weap))
                unowned.Add(weap);
        }
        return unowned;
    }
}
