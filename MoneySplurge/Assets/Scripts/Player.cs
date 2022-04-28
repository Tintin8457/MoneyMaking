using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [Header("Player")]
    public float speed = 5.0f;
    private Rigidbody2D playerRigidBody;
    public Animator playerAnim;
    public SpriteRenderer sprite;
    public int jumpForce = 7;
    private int jumpAmount = 0;
    private int maxJump = 1;
    public bool readyToPlay = false;

    [Header("Rent")]
    public float rentAmount = 11.00f;
    public float averageRent = 1175.00f;
    public float currentRent = 0.00f;
    public bool hasPaid = false;

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
    public float speedDuration = 10.0f;
    public bool increasedSpeed = false;

    [Header("Other Hoops")]
    public int ticket = 15;
    public int carBill = 200;
    public bool ticketPaid = false;
    public bool paidCar = false;
    public bool paidCop = false;
    public bool debuffsOff = false;
    public float debuffDuration = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        payChoices.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        currentRentText.text = "Bills: $" + currentRent.ToString("F2");
        roundText.text = "Month: " + round.ToString() + "/" + maxRounds.ToString();

        if (currentRent < 0f)
        {
            currentRentText.color = new Color32(255, 0, 0, 255);
        }

        else if (currentRent >= 0f)
        {
            currentRentText.color = new Color32(255, 255, 255, 255);
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
            else if (speedDuration <= 0f)
            {
                increasedSpeed = false;
            }
        }

        //Reset the normal speed and normal rent earn amount
        else if (increasedSpeed == false)
        {
            speed = 5;
            rentAmount = 11.00f;
            speedDuration = 10.0f;
        }

        //Debuffs are removed
        if (debuffsOff == true)
        {
            //Countdown until the increased speed use is up
            if (debuffDuration > 0f)
            {
                debuffDuration -= Time.deltaTime;
            }

            //Speed Pickup is done being used
            if (debuffDuration <= 0f)
            {
                debuffsOff = false;
            }
        }

        //Debuffs are back
        else if (debuffsOff == false)
        {
            debuffDuration = 10.0f;

            paidCar = true;
            paidCop = true;
        }

        //Jump only once
        if (readyToPlay == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (jumpAmount > 0)
                {
                    ChangePlayerState(2);
                    playerRigidBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                    jumpAmount -= 1;
                }
            }
        }

        if (readyToPlay == true && Time.timeScale == 1)
        {
            //Anims
            if (Input.GetKeyUp(KeyCode.A))
            {
                ChangePlayerState(0);
            }

            else if (Input.GetKeyDown(KeyCode.A))
            {
                ChangePlayerState(1);
                sprite.flipX = true;
            }

            if (Input.GetKeyUp(KeyCode.D))
            {
                ChangePlayerState(0);
            }

            else if (Input.GetKeyDown(KeyCode.D))
            {
                ChangePlayerState(1);
                sprite.flipX = false;
            }
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void FixedUpdate()
    {
        //Move with A and D in game
        if (readyToPlay == true)
        {
            if (Input.GetKey(KeyCode.A))
            {
                transform.position += Time.deltaTime * speed * -transform.right;
            }

            if (Input.GetKey(KeyCode.D))
            {
                transform.position += Time.deltaTime * speed * transform.right;
            }
        }
    }

    //Only allow the player to jump once with the spacebar
    private void OnCollisionEnter2D(Collision2D ground)
    {
        if (ground.gameObject.CompareTag("Ground"))
        {
            jumpAmount = maxJump;
            ChangePlayerState(0);
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
            currentRent -= ticket;
            paidCop = true;
            Destroy(game.gameObject);
        }

        //Pay a $200 bill if they get a car
        if (game.CompareTag("Car"))
        {
            currentRent -= carBill;
            paidCar = true;
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
        currentRent += rent;
    }

    //Increase speed of the player
    public void IncreaseSpeed(float newSpeed)
    {
        speed += newSpeed;
        increasedSpeed = true;
    }

    //Change state of the player
    public void ChangePlayerState(int state)
    {
        playerAnim.SetInteger("ChangeType", state);
    }
}
