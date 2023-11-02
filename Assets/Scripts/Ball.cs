using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    GameManager manager;

    private void Start()
    {
        manager = FindObjectOfType<GameManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Check for collision
        //Debug.Log("Ball collided with " + collision.gameObject.name);

        //Filter out the objects of interest.
        if(collision.gameObject.CompareTag("Pin"))
        {
            Debug.Log("Collided with a pin");
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Ball entered the pit");
        manager.SetNextThrow();

        Destroy(gameObject);
    }
}
