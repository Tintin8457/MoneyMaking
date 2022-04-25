using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PayUp : MonoBehaviour
{
    private Player player;
    public float creditCard = 0;
    public GameObject notEnough;
    public TextMeshProUGUI lowAmountMessage;

    // Start is called before the first frame update
    void Start()
    {
        notEnough.SetActive(false);
        lowAmountMessage.text = "";

        GameObject taxPayer = GameObject.FindGameObjectWithTag("Player");

        if (taxPayer != null)
        {
            player = taxPayer.GetComponent<Player>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //If all funds are available, pay the funds
    public void PayFunds()
    {
        if (player.currentRent >= player.averageRent)
        {
            player.currentRent -= player.averageRent;

            lowAmountMessage.text = "Your rent has been paid!";
            notEnough.SetActive(true);
        }

        //Cannot pay because lack of funds
        else
        {
            lowAmountMessage.text = "You don't have enough funds to pay rent!";
            notEnough.SetActive(true);
        }
    }

    public void CreditCard()
    {
        //Add the current money to credit card debt
        if (player.currentRent > 0)
        {
            creditCard += player.currentRent;
            player.currentRent -= creditCard;

            lowAmountMessage.text = "Your added money into the credit card!";
            notEnough.SetActive(true);
        }

        //Add the current money to credit card debt
        else if (player.currentRent == 0)
        {
            lowAmountMessage.text = "You don't have enough money to add to your debt!";
            notEnough.SetActive(true);
        }
    }

    //If all funds are available, pay the funds
    public void ExitDialog()
    {
        notEnough.SetActive(false);
    }

    //Exit from pay prompt
    public void Exit()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
        player.round++;
        player.canDestroyBarrier = true;
    }
}
