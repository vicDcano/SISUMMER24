using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumSwing : MonoBehaviour
{
    public float slowdownRate = 0.98f; // Adjust this value for desired slowdown rate
    public float minVelocity = 0.1f;   // The minimum velocity threshold to stop the pendulum

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (rb.velocity.magnitude > minVelocity)
        {
            rb.velocity *= slowdownRate;
        }
        else
        {
            rb.velocity = Vector3.zero;
        }

        if (rb.angularVelocity.magnitude > minVelocity)
        {
            rb.angularVelocity *= slowdownRate;
        }
        else
        {
            rb.angularVelocity = Vector3.zero;
        }
    }
}
