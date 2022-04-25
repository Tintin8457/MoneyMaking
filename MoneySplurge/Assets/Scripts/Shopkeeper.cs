using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shopkeeper : MonoBehaviour
{
    public GameObject shop;
    public GameObject purchaseConfirmation;
    public TextMeshProUGUI confirmationText;

    public float speedIncrease = 3.0f;

    public bool option1 = false;
    public bool option2 = false;

    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        shop.SetActive(false);
        purchaseConfirmation.SetActive(false);

        GameObject taxPayer = GameObject.FindGameObjectWithTag("Player");

        if (taxPayer != null)
        {
            player = taxPayer.GetComponent<Player>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Player chooses the first option
        if (option1 == true)
        {
            //The player will have an increased rent gain
            if (player.currentRent >= 8.00f)
            {
                player.rentIncrease = true;
                player.currentRent -= 8.00f;

                confirmationText.text = "You have purchased an incresed rent gain upgrade!";
                purchaseConfirmation.SetActive(true);
                option1 = false;
            }

            //Display a pop-up that the player cannot buy the upgrade
            else if (player.currentRent < 8.00f)
            {
                confirmationText.text = "You don't have enough money to buy the selected upgrade!";
                purchaseConfirmation.SetActive(true);
                option1 = false;
            }
        }

        //Player chooses the second option
        if (option2 == true)
        {
            //The player will have an increased speed
            if (player.currentRent >= 15.00f)
            {
                player.IncreaseSpeed(speedIncrease);
                player.currentRent -= 15.00f;

                confirmationText.text = "You have purchased a speed increase!";
                purchaseConfirmation.SetActive(true);
                //option2 = false;
            }

            //Display a pop-up that the player cannot buy the upgrade
            else if (player.currentRent < 15.00f)
            {
                confirmationText.text = "You don't have enough money to buy the selected upgrade!";
                purchaseConfirmation.SetActive(true);
                option2 = false;
            }
        }
    }

    //Open up shop when the player is nearby
    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.CompareTag("Player"))
        {
            shop.SetActive(true);
        }
    }

    //Close the shop when the player is gone
    private void OnTriggerExit2D(Collider2D player)
    {
        if (player.CompareTag("Player"))
        {
            shop.SetActive(false);
            purchaseConfirmation.SetActive(false);
        }
    }

    //Check if the player has $8 to purchase that allows the player earn rent by 2x
    public void Option1()
    {
        option1 = true;
    }

    //Check if the player has $15 to purchase to increase their speed
    public void Option2()
    {
        option2 = true;
    }

    //Remove cannot buy upgrade pop-up
    public void ExitConfirmation()
    {
        purchaseConfirmation.SetActive(false);
    }
}
