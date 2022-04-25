using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [Header("Player")]
    public float speed = 5.0f;
    private Rigidbody2D playerRigidBody;
    public int jumpForce = 7;
    
    private int jumpAmount = 0;
    private int maxJump = 1;

    [Header("Rent")]
    public float rentAmount = 11.00f;
    public float averageRent = 1175.00f;
    public float currentRent = 0.00f;

    [Header("Round")]
    public int round = 1;
    public int maxRounds = 2;
    public bool canDestroyBarrier = false;
    public TextMeshProUGUI roundText;
    public GameObject payChoices;

    [Header("UI")]
    public TextMeshProUGUI currentRentText;

    [Header("Speed PowerUp")]
    public float speedIncrease = 3.0f;
    public float rentEarnDecrease = 3.00f;
    public float speedDuration = 5.0f;
    public bool increasedSpeed = false;

    [Header("Shop")]
    public bool rentIncrease = false;

    [Header("Other Hoops")]
    public int tickets = 0;
    public int carBill = 200;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        payChoices.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        currentRentText.text = "Current Rent: $" + currentRent.ToString("F2");
        roundText.text = "Round: " + round.ToString() + "/" + maxRounds.ToString();

        //Jump only once
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpAmount > 0)
            {
                playerRigidBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                jumpAmount -= 1;
            }
        }

        //Increased Speed pickup is being used
        if (increasedSpeed == true)
        {
            //Countdown until the increased speed use is up
            if (speedDuration > 0f)
            {
                speedDuration -= Time.deltaTime;
            }

            //Speed Pickup is done being used
            if (speedDuration <= 0f)
            {
                increasedSpeed = false;
            }
        }

        //Reset the normal speed and normal rent earn amount
        else if (increasedSpeed == false)
        {
            speed = 5;
            rentAmount = 11.00f;
            speedDuration = 5.0f;
        }
    }

    void FixedUpdate()
    {
        //Move with A and D in game
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += -transform.right * Time.deltaTime * speed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * Time.deltaTime * speed;
        }
    }

    //Only allow the player to jump once with the spacebar
    private void OnCollisionEnter2D(Collision2D ground)
    {
        if (ground.gameObject.CompareTag("Ground"))
        {
            jumpAmount = maxJump;
        }
    }

    private void OnTriggerEnter2D(Collider2D game)
    {
        //Only allow the player to jump once with the spacebar
        if (game.CompareTag("Ground"))
        {
            jumpAmount = maxJump;
        }

        //Earn cash after collecting rent
        if (game.CompareTag("Money"))
        {
            EarnRent(rentAmount);
            Destroy(game.gameObject);
        }

        //Collect a power-up that increases the player's speed and decreases the amount of rent they are collecting
        if (game.CompareTag("Faster"))
        {
            IncreaseSpeed(speedIncrease);
            rentAmount -= rentEarnDecrease;
            increasedSpeed = true;
            Destroy(game.gameObject);
        }

        //Get a ticket for getting a siren
        if (game.CompareTag("Siren"))
        {
            tickets += 1;
            Destroy(game.gameObject);
        }

        //Pay a $200 bill if they get a car
        if (game.CompareTag("Car"))
        {
            currentRent -= carBill;
            Destroy(game.gameObject);
        }

        //Stop to pay when they reached the end of the level
        if (game.CompareTag("StopToPay"))
        {
            payChoices.SetActive(true);
            Time.timeScale = 0;

            if (canDestroyBarrier == true)
            {
                Destroy(game.gameObject);
                canDestroyBarrier = false;
            }
        }
    }

    //Destroy the hoop once the player has exited it
    private void OnTriggerExit2D(Collider2D hoop)
    {
        if (hoop.CompareTag("Hoop"))
        {
            Destroy(hoop.gameObject, 0.5f);
        }
    }

    //Earn rent after collecting cash
    public void EarnRent(float rent)
    {
        //Earn the normal amount of rent
        if (rentIncrease == false)
        {
            currentRent += rent;
        }

        //Earn the double amount of rent
        else if (rentIncrease == true)
        {
            currentRent += rent * 2;
        }
    }

    //Increase speed of the player
    public void IncreaseSpeed(float newSpeed)
    {
        speed += newSpeed;
        Debug.Log(speed);
    }
}