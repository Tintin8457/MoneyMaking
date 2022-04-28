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
    public bool option1Purchased = false;
    public bool option2 = false;
    public bool salesMade = false;

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
        if (salesMade == true)
        {
            StartCoroutine(ResetShop());
        }

        //Player chooses the first option
        if (option1 == true)
        {
            //The player has paid to remove their debuffs
            if (player.currentRent > 100.00f)
            {
                if (player.paidCar == true || player.paidCop == true)
                {
                    player.currentRent -= 100.00f;
                    confirmationText.text = "The debuffs are now removed!";
                    purchaseConfirmation.SetActive(true);
                    option1Purchased = true;
                    option1 = false;
                }
            }

            else if (player.currentRent <= 100.00f)
            {
                confirmationText.text = "You don't have enough money to remove debuffs!";
                purchaseConfirmation.SetActive(true);
                option1 = false;
            }
        }

        //Player chooses the second option
        if (option2 == true)
        {
            //The player will have an increased speed
            if (player.currentRent >= 100.00f)
            {
                player.IncreaseSpeed(speedIncrease);
                player.currentRent -= 100.00f;

                confirmationText.text = "You have purchased a speed increase!";
                purchaseConfirmation.SetActive(true);
                option2 = false;
            }

            //Display a pop-up that the player cannot buy the upgrade
            else if (player.currentRent < 100.00f)
            {
                confirmationText.text = "You don't have enough money to buy the speed boost!";
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
            if (salesMade == false)
            {
                shop.SetActive(true);
                Time.timeScale = 0;
            }

            else if (salesMade == true)
            {
                shop.SetActive(false);
                Time.timeScale = 1;
            }
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
        Time.timeScale = 1;
        shop.SetActive(false);
        salesMade = true;

        if (option1Purchased == true)
        {
            player.debuffsOff = true;
            option1Purchased = false;
            player.paidCar = false;
            player.paidCop = false;
        }
    }

    IEnumerator ResetShop()
    {
        yield return new WaitForSeconds(5f);
        salesMade = false;
    }
}
