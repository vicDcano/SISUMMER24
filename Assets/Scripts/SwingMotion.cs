using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingMotion : MonoBehaviour
{

    public Transform ball; // Reference to the pendulum's ball
    public float ropeLength = 1.479f; // Length of the pendulum's rope
    public float gravity = 9.81f; // Acceleration due to gravity


    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    void FixedUpdate()
    {
        Vector3 pivotToBall = ball.position - transform.position;
        float distance = pivotToBall.magnitude;

        // Calculate the force acting on the ball (tension in the rope)
        Vector3 tension = -pivotToBall.normalized * (distance - ropeLength) * (gravity / ropeLength);

        // Apply the force to the ball's Rigidbody
        ball.GetComponent<Rigidbody>().AddForce(tension, ForceMode.Force);
    }

}
