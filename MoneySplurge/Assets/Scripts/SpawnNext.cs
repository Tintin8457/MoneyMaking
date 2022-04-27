using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNext : MonoBehaviour
{
    public GameObject hoopSpawner;

    // Start is called before the first frame update
    void Start()
    {
        hoopSpawner.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Spawn next wave
    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.CompareTag("Player"))
        {
            hoopSpawner.SetActive(true);
        }
    }

    //Destroy the spawn spot once the player has exited it
    private void OnTriggerExit2D(Collider2D spawn)
    {
        if (spawn.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
