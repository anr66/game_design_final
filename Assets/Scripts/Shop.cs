using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public Text moneyText;

    public GameObject basicTowerPrefab;
    GameObject boughtTower;

    public SteamVR_TrackedController tc;


    // Update is called once per frame
    void Update()
    {
        moneyText.text = "Money: $" + Money.Amount;

        if (boughtTower != null)
        {
            MovePurchasedTower();
            CheckForWall();
        }
    }

    void MovePurchasedTower()
    {
        // tower in fromt of camera
        boughtTower.transform.position = Camera.main.transform.position +
            Camera.main.transform.forward;
    }

    void CheckForWall()
    {
        // raycast
        Ray raycast = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        //Debug.DrawRay(raycast.origin, raycast.direction * 100);

        // check if the raycast hits something
        if (Physics.Raycast(raycast, out hit))
        {
            if (tc.triggerPressed)
            {
                // hit a wall?
                if (hit.collider.gameObject.tag == "Wall")
                {
                    //put tower on top wall
                    boughtTower.transform.position = hit.collider.gameObject.transform.position + new Vector3(0.01f, -0.13f, 0.09f);
                    //boughtTower.transform.position = hit.collider.gameObject.transform.position;// + new Vector3(0f, 0f, 0f);


                    // disable wall tile
                    hit.collider.gameObject.tag = "Untagged";

                    // make tower transparent
                    Color color = boughtTower.GetComponent<Renderer>().material.color;
                    color.a = 1f;
                    boughtTower.GetComponent<Renderer>().material.color = color;

                    // enable script
                    boughtTower.GetComponent<TowerScript>().enabled = true;

                    // disconnect from the tower to buy another
                    boughtTower = null;

                }
            }
        }
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
