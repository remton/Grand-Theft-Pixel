using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Car")]
public class Car : ScriptableObject
{
    public string carName;
    public string discription;
    public int id;

    public int cost;
    public Sprite sprite;
    public int health;
    public int speed;
    public int acceleration;

    public AnimatorOverrideController animOverride;

    public static Car FindCar(int id)
    {
        foreach (Car car in GameData.instance.allCars)
        {
            if (car.id == id)
                return car;
        }
        Debug.LogError("Unable to find car with id: " + id.ToString());
        return null;
    }

    /*      CAR IDs
     * -----------------
     * RedCar: 1
     * BlueTruck: 2
     * PoliceCar:
     */

}
