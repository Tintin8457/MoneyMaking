using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debuffers : MonoBehaviour
{
    private Player player;

    // Start is called before the first frame update
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
        if (player.debuffsOff == true)
        {
            gameObject.SetActive(false);
        }

        else if (player.debuffsOff == false)
        {
            gameObject.SetActive(true);
        }
    }
}
