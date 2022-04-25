using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopSpawner : MonoBehaviour
{
    public GameObject hoop;
    public int spawnAmount = 2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnAmount == 2)
        {
            hoop.transform.position = new Vector2(transform.position.x, transform.position.y);
            Instantiate(hoop);
            spawnAmount -= 1;
        }

        else if (spawnAmount == 1)
        {
            hoop.transform.position = new Vector2(transform.position.x + 4, transform.position.y);
            Instantiate(hoop);
            spawnAmount -= 1;
        }
    }
}
