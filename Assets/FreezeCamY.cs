using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeCamY : MonoBehaviour
{
    private Rigidbody rb;
    private float initialYPos;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        initialYPos = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = transform.position;
        newPos.y = initialYPos;
        transform.position = newPos;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //rb.constraints = RigidbodyConstraints.FreezePositionY;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        initialYPos = transform.position.y;
    }
}
