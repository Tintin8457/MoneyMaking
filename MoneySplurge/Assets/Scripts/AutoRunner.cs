using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRunner : MonoBehaviour
{
    public float speed = 3.0f;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(speed * Time.deltaTime * transform.right);
    }
}
