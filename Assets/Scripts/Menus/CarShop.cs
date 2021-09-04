using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarShop : MonoBehaviour {

    private List<Car> cars = new List<Car>(); // List of all the cars in the game
    public GameObject previewObj;

    public Text nameTxt;
    public Text descriptionTxt;
    public Text costTxt;
    public Text healthTxt;
    public Text speedTxt;
    public Text bankTxt;
    public GameObject purchaseButton;

    int selectedCar = 0;
    
    public void OpenShop()
    {
        cars = PlayerData.GetUnownedCars();
        selectedCar = 0;
        UpdateMenu();
    }

    public void MoveCarSelectRight()
    {
        if (selectedCar + 1 < cars.Count)
        {
            selectedCar = selectedCar + 1;
        }
        UpdateMenu();
    }
    public void MoveCarSelectLeft()
    {
        if (selectedCar - 1 >= 0)
        {
            selectedCar = selectedCar - 1;
        }
        UpdateMenu();
    }

    private void UpdateMenu()
    {
        if (cars.Count <= 0)
        {
            // Display out of stock
            Debug.Log("ALL CARS OWNED");
            previewObj.GetComponent<Image>().sprite = null;
            nameTxt.text = "Out of Stock";
            descriptionTxt.text = "You've bought all the Cars!";
            costTxt.text = "Cost: n/a";
            healthTxt.text = ("Health: N/A");
            speedTxt.text = ("Speed: N/A");
            bankTxt.text = ("Bank: $" + PlayerData.instance.bank.ToString());
            purchaseButton.GetComponent<Image>().color = new Color(0.53f, 0.64f, 0.56f); //gray color;
            purchaseButton.GetComponentInChildren<Text>().text = "Out of Stock";
            return;
        }
        previewObj.GetComponent<Image>().sprite = cars[selectedCar].sprite;
        nameTxt.text = cars[selectedCar].carName;
        descriptionTxt.text = cars[selectedCar].discription;
        costTxt.text = ("Cost: $" + cars[selectedCar].cost.ToString());
        healthTxt.text = ("Health: " + cars[selectedCar].health.ToString());
        speedTxt.text = ("Speed: " + cars[selectedCar].speed.ToString());
        bankTxt.text = ("Bank: $" + PlayerData.instance.bank.ToString());

        if (PlayerData.IsOwned(cars[selectedCar]))
        {
            purchaseButton.GetComponent<Image>().color = new Color(0.53f, 0.64f, 0.56f); //gray color
            purchaseButton.GetComponentInChildren<Text>().text = "owned";
        }
        else if (PlayerData.instance.bank >= cars[selectedCar].cost)
        {
            purchaseButton.GetComponent<Image>().color = new Color(0.22f, 0.88f, 0.06f); //green color
            purchaseButton.GetComponentInChildren<Text>().text = "buy!";
        }
        else
        {
            purchaseButton.GetComponent<Image>().color = new Color(0.79f, 0.20f, 0.20f); //red color
            purchaseButton.GetComponentInChildren<Text>().text = "buy!";
        }
    }

    public void PurchaseCar()
    {
        if (cars.Count <=0 || PlayerData.IsOwned(cars[selectedCar]))
            return;

        if (PlayerData.instance.bank >= cars[selectedCar].cost)
        {
            PlayerData.instance.bank -= cars[selectedCar].cost;
            PlayerData.instance.ownedCars.Add(cars[selectedCar]);
            PlayerData.instance.activeCar = cars[selectedCar];
            PlayerData.Save();
        }
        else
            Debug.Log("insufficient funds");
        UpdateMenu();
    }
}
