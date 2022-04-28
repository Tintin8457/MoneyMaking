using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PayUp : MonoBehaviour
{
    private Player player;
    public GameObject notEnough;
    public TextMeshProUGUI lowAmountMessage;
    public Sam uncleSam;
    public TextMeshProUGUI backButton;

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

    //If all funds are available, pay the funds
    public void PayFunds()
    {
        if (player.currentRent >= player.averageRent)
        {
            player.currentRent -= player.averageRent;

            lowAmountMessage.text = "Your rent has been paid!";
            notEnough.SetActive(true);
            uncleSam.Win();
        }

        //Cannot pay because lack of funds
        else
        {
            lowAmountMessage.text = "You're broke!";
            notEnough.SetActive(true);
            uncleSam.Lose();
        }

        if (uncleSam.win == true || uncleSam.lose == true)
        {
            backButton.text = "Quit";
        }
    }

    //Exit from pay prompt
    public void Exit()
    {
        if (uncleSam.win == false && uncleSam.lose == false)
        {
            notEnough.SetActive(false);
            gameObject.SetActive(false);
            Time.timeScale = 1;
            player.round++;
            player.canDestroyBarrier = true;
            player.hasPaid = true;
        }

        else if (uncleSam.win == true || uncleSam.lose == true)
        {
            Application.Quit();
        }
    }
}
