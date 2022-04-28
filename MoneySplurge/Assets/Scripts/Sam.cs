using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Sam : MonoBehaviour
{
    public TextMeshProUGUI dialgoue;
    public float readTime = 7.0f;
    public float reset = 7.0f;
    public bool canRead = true;
    public bool lose = false;
    public bool win = false;

    public GameObject gameView;
    public GameObject barrier;
    public GameObject bg;
    public GameObject speechBubble;

    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        dialgoue.text = "Hey patriot! Rent is coming and you need to pay your bills! Collect cash to pay it off by the end of the month and avoid getting caught by the cops. Sometimes, your car will break down if you hit this, you'll have to pay for repairs! Now go forth and pay your taxes!";

        GameObject taxPayer = GameObject.FindGameObjectWithTag("Player");

        if (taxPayer != null)
        {
            player = taxPayer.GetComponent<Player>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canRead == true)
        {
            gameView.GetComponent<AutoRunner>().enabled = false;
            barrier.GetComponent<AutoRunner>().enabled = false;
            bg.GetComponent<AutoRunner>().enabled = false;
            speechBubble.SetActive(true);
            player.readyToPlay = false;

            if (readTime >= 0.0f)
            {
                readTime -= Time.deltaTime;
            }

            if (readTime < 0.0f)
            {
                canRead = false;
                readTime = reset;
            }
        }

        else if (canRead == false)
        {
            gameView.GetComponent<AutoRunner>().enabled = true;
            barrier.GetComponent<AutoRunner>().enabled = true;
            bg.GetComponent<AutoRunner>().enabled = true;
            speechBubble.SetActive(false);
            dialgoue.text = "";
            player.readyToPlay = true;
        }

        if (lose == true)
        {
            dialgoue.text = "In Florida, minimum wage is $11 an hour, and the average rent is $1,175. This is not sustainable, change needs to happen.";
            speechBubble.SetActive(true);
            gameView.GetComponent<AutoRunner>().enabled = false;
            barrier.GetComponent<AutoRunner>().enabled = false;
            bg.GetComponent<AutoRunner>().enabled = false;

            if (player.round < 2)
            {
                player.round++;
            }
        }

        if (win == true)
        {
            dialgoue.text = "Congratulations, you barely scraped by like many hard-working Americans. In Florida, minimum wage is $11 an hour, and the average rent is $1,175. This is not sustainable, change needs to happen.";
            speechBubble.SetActive(true);
            gameView.GetComponent<AutoRunner>().enabled = false;
            barrier.GetComponent<AutoRunner>().enabled = false;
            bg.GetComponent<AutoRunner>().enabled = false;

            if (player.round < 2)
            {
                player.round++;
            }
        }

        if (player.hasPaid == true)
        {
            speechBubble.SetActive(false);
        }
    }

    public void Lose()
    {
        lose = true;
    }

    public void Win()
    {
        win = true;
    }
}
