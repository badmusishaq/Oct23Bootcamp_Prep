using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    public bool isFallen;

    Vector3 startPosition;
    Quaternion startRotation;

    Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if(gameObject.activeSelf)
            isFallen = Quaternion.Angle(startRotation, transform.localRotation) > 5;*/

        if(gameObject.activeSelf)
        {
            isFallen = Quaternion.Angle(startRotation, transform.localRotation) > 5;
        }
    }

    public void ResetPin()
    {
        gameObject.SetActive(true);
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;

        transform.position = startPosition + Vector3.up * 0.01f;
        transform.rotation = startRotation;
        isFallen = false;
        rb.isKinematic = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pit"))
        {
            isFallen = true;
        }
    }
}
