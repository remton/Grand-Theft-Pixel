using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GarageScript : MonoBehaviour
{
    [Header("Weapon Selection")]
    public Text weapNameTxt;
    public Text weapDiscritptionTxt;
    public Text fireRateTxt;
    public Text magSizeTxt;
    public Text reloadSpeedTxt;
    public GameObject weapPreview;
    public GameObject weapSelectButton;

    [Header("Car Selection")]
    public Text carNameTxt;
    public Text carDiscriptionTxt;
    public Text carSpeedTxt;
    public Text carHealthTxt;
    public GameObject carPreview;
    public GameObject carSelectButton;

    private List<Weapon> ownedWeapons;
    private int selectedWeapon;

    private List<Car> ownedCars;
    private int selectedCar;

    private void Start()
    {
        UpdateMenu();
    }
    public void UpdateMenu()
    {
        ownedWeapons = PlayerData.instance.ownedWeapons;
        ownedCars = PlayerData.instance.ownedCars;

        weapNameTxt.text = ownedWeapons[selectedWeapon].weaponName;
        weapDiscritptionTxt.text = ownedWeapons[selectedWeapon].discription;
        fireRateTxt.text = ("Fire Rate: " + ownedWeapons[selectedWeapon].fireRate.ToString());
        magSizeTxt.text = ("Mag Size: " + ownedWeapons[selectedWeapon].magSize.ToString());
        reloadSpeedTxt.text = ("Reload Speed: " + ownedWeapons[selectedWeapon].reloadSpeed.ToString());
        weapPreview.GetComponent<Image>().sprite = ownedWeapons[selectedWeapon].sprite;
        if (PlayerData.instance.activeWeapon == ownedWeapons[selectedWeapon])
        {
            weapSelectButton.GetComponent<Image>().color = new Color(0.53f, 0.64f, 0.56f); //gray color
            weapSelectButton.GetComponentInChildren<Text>().text = "equipped";
        }
        else
        {
            weapSelectButton.GetComponent<Image>().color = new Color(0.22f, 0.88f, 0.06f); //green color
            weapSelectButton.GetComponentInChildren<Text>().text = "select";
        }

        carNameTxt.text = ownedCars[selectedCar].carName;
        carDiscriptionTxt.text = ownedCars[selectedCar].discription;
        carSpeedTxt.text = ("Speed: " + ownedCars[selectedCar].speed.ToString());
        carHealthTxt.text = ("Health: " + ownedCars[selectedCar].health.ToString());
        carPreview.GetComponent<Image>().sprite = ownedCars[selectedCar].sprite;
        if (PlayerData.instance.activeCar == ownedCars[selectedCar])
        {
            carSelectButton.GetComponent<Image>().color = new Color(0.53f, 0.64f, 0.56f); //gray color
            carSelectButton.GetComponentInChildren<Text>().text = "equipped";
        }
        else
        {
            carSelectButton.GetComponent<Image>().color = new Color(0.22f, 0.88f, 0.06f); //green color
            carSelectButton.GetComponentInChildren<Text>().text = "select";
        }
    }

    public void WeaponSelectRight()
    {
        if(selectedWeapon + 1 < ownedWeapons.Count)
        {
            selectedWeapon += 1;
        }
        UpdateMenu();
    }
    public void WeaponSelectLeft()
    {
        if (selectedWeapon - 1 >= 0)
        {
            selectedWeapon -= 1;
        }
        UpdateMenu();
    }
    public void SelectWeapon()
    {
        PlayerData.instance.activeWeapon = ownedWeapons[selectedWeapon];
        PlayerData.Save();
        UpdateMenu();
    }

    public void CarSelectRight()
    {
        if (selectedCar + 1 < ownedCars.Count)
        {
            selectedCar += 1;
        }
        UpdateMenu();
    }
    public void CarSelectLeft()
    {
        if (selectedCar - 1 >= 0)
        {
            selectedCar -= 1;
        }
        UpdateMenu();
    }
    public void SelectCar()
    {
        PlayerData.instance.activeCar = ownedCars[selectedCar];
        PlayerData.Save();
        UpdateMenu();
    }
}
