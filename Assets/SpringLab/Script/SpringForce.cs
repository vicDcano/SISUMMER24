using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class SpringForce : MonoBehaviour
{
    /*public Rigidbody rb;
    public GameObject gameObj;
    private float length;*/


    private Vector3 velocity;

    public void Start()
    {
    }

    public void Update()
    {
       
    }

    /*public void FixedUpdate()
    {
        RaycastHit hit;

        if(Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.up), out hit, length))
        {
            float forceAmount = 0;

            forceAmount = (Mathf.Pow(length - hit.distance, 2f) / length * strength) / length;
            rb.AddForceAtPosition(transform.up * forceAmount, transform.position);
        }
    }*/
}
