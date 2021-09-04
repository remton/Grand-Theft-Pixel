using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData instance;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        instance = this;
    }

    public Car startCar;
    public Weapon startWeapon;
    public List<Car> allCars;
    public List<Weapon> allWeapons;
}
