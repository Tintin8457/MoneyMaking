using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopSpawner : MonoBehaviour
{
    public int spawnAmount = 2;

    [Header("Hoops")]
    public GameObject[] hoop;

    [Header("Extra numbers")]
    public int[] x;
    public int[] y;

    private Player player;

    void Start()
    {
        GameObject taxPayer = GameObject.FindGameObjectWithTag("Player");

        if (taxPayer != null)
        {
            player = taxPayer.GetComponent<Player>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnAmount == 3)
        {
            hoop[0].transform.position = new Vector2(transform.position.x + x[0], transform.position.y + y[0]);
            Instantiate(hoop[0]);
            spawnAmount -= 1;
        }

        else if (spawnAmount == 2)
        {
            hoop[1].transform.position = new Vector2(transform.position.x + x[1], transform.position.y + y[1]);
            Instantiate(hoop[1]);
            spawnAmount -= 1;
        }

        else if (spawnAmount == 1)
        {
            hoop[2].transform.position = new Vector2(transform.position.x + x[2], transform.position.y + y[2]);
            Instantiate(hoop[2]);
            spawnAmount -= 1;
        }
    }
}
