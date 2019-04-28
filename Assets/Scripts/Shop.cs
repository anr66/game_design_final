using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public Text moneyText;

    public GameObject basicTowerPrefab;
    GameObject boughtTower;

    // Update is called once per frame
    void Update()
    {
        moneyText.text = "Money: $" + Money.Amount;

        if (boughtTower != null)
        {
            MovePurchasedTower();
        }
    }

    void MovePurchasedTower()
    {
        // tower in fromt of camera
        boughtTower.transform.position = Camera.main.transform.position +
            Camera.main.transform.forward
            
            ;
    }

    public void BuyBasicTower()
    {
        // not enough money
        if (Money.Amount < 40 || boughtTower != null)
        {
            return;
        }

        // spawn new tower
        boughtTower = Instantiate(basicTowerPrefab, transform.position, Quaternion.identity);

        // make tower transparent
        Color color = boughtTower.GetComponent<Renderer>().material.color;
        color.a = 0.5f;
        boughtTower.GetComponent<Renderer>().material.color = color;

        // take away money
        Money.Amount -= 40;

        Debug.Log("button clicked");
    }

}
