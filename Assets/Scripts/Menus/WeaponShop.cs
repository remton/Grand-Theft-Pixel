using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponShop : MonoBehaviour
{
    private List<Weapon> weapons; //List of all the weapons in the game
    public GameObject previewObj;

    public Text nameTxt;
    public Text descriptionTxt;
    public Text costTxt;
    public Text fireRateTxt;
    public Text magSizeTxt;
    public Text reloadSpeedTxt;
    public Text bankTxt;
    public GameObject purchaseButton;

    int selectedWeapon = 0;
    
    public void OpenShop()
    {
        weapons = PlayerData.GetUnownedWeapons();
        selectedWeapon = 0;
        UpdateMenu();
    }

    public void MoveSelectionRight()
    {
        if (selectedWeapon + 1 < weapons.Count)
        {
            selectedWeapon = selectedWeapon + 1;
        }
        UpdateMenu();
    }
    public void MoveSelectionLeft()
    {
        if (selectedWeapon - 1 >= 0)
        {
            selectedWeapon = selectedWeapon - 1;
        }
        UpdateMenu();
    }

    private void UpdateMenu()
    {
        if(weapons.Count <= 0)
        {
            // Display out of stock
            Debug.Log("ALL WEAPONS OWNED");
            previewObj.GetComponent<Image>().sprite = null;
            nameTxt.text = "Out of Stock";
            descriptionTxt.text = "You've bought all the weapons!";
            costTxt.text = "Cost: n/a";
            fireRateTxt.text = "Fire Rate: N/A";
            magSizeTxt.text = "Mag Size: N/A";
            reloadSpeedTxt.text = "Reload Speed: N/A";
            bankTxt.text = ("Bank: $" + PlayerData.instance.bank.ToString());
            purchaseButton.GetComponent<Image>().color = new Color(0.53f, 0.64f, 0.56f); //gray color;
            purchaseButton.GetComponentInChildren<Text>().text = "Out of Stock";
            return;
        }

        Weapon displayWeap = weapons[selectedWeapon];
        previewObj.GetComponent<Image>().sprite = displayWeap.sprite;
        nameTxt.text = displayWeap.weaponName;
        descriptionTxt.text = displayWeap.discription;
        costTxt.text = ("Cost: $" + displayWeap.cost.ToString());
        fireRateTxt.text = ("Fire Rate: " + displayWeap.fireRate.ToString());
        magSizeTxt.text = ("Mag Size: " + displayWeap.magSize.ToString());
        reloadSpeedTxt.text = ("Reload Speed: " + displayWeap.reloadSpeed.ToString());
        bankTxt.text = ("Bank: $" + PlayerData.instance.bank.ToString());

        if (PlayerData.IsOwned(displayWeap))
        {
            purchaseButton.GetComponent<Image>().color = new Color(0.53f, 0.64f, 0.56f); //gray color
            purchaseButton.GetComponentInChildren<Text>().text = "owned";
        }
        else if (PlayerData.instance.bank >= displayWeap.cost)
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

    public void PurchaseWeapon()
    {
        if (weapons.Count <= 0 || PlayerData.IsOwned(weapons[selectedWeapon]))
            return;

        if (PlayerData.instance.bank >= weapons[selectedWeapon].cost)
        {
            PlayerData.instance.bank -= weapons[selectedWeapon].cost;
            PlayerData.instance.ownedWeapons.Add(weapons[selectedWeapon]);
            PlayerData.instance.activeWeapon = weapons[selectedWeapon];
            PlayerData.Save();
        }
        else
            Debug.Log("insufficient funds");
        UpdateMenu();
    }
}
