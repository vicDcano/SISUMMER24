using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit;

public class SpringInteraction : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    private SpringJoint springJoint;
    private Rigidbody rb;
    private Vector3 initialPosition;
    private float originalSpring;
    private bool isGrabbed = false;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        springJoint = GetComponent<SpringJoint>();
        rb = GetComponent<Rigidbody>();
        initialPosition = transform.position;
        originalSpring = springJoint.spring;
    }

    private void OnEnable()
    {
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnDisable()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        // Disable the spring effect when grabbed
        springJoint.spring = 0;
        isGrabbed = true;

        // Constrain the rigidbody to move only along the x-axis and prevent rotation
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ |
                         RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY |
                         RigidbodyConstraints.FreezeRotationZ;
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        // Re-enable the spring effect when released
        springJoint.spring = originalSpring;
        isGrabbed = false;

        // Reset constraints after release
        rb.constraints = RigidbodyConstraints.None;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void FixedUpdate()
    {
        if (isGrabbed)
        {
            // Keep the object's position constrained to the x-axis while grabbed
            Vector3 currentPosition = transform.position;
            currentPosition.y = initialPosition.y;  // Keep Y position constant
            currentPosition.z = initialPosition.z;  // Keep Z position constant
            rb.MovePosition(currentPosition);
        }

        // Handle collision with a wall
        if (Vector3.Distance(transform.position, initialPosition) < 0.1f)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            transform.position = initialPosition;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            // Stop the object if it hits the wall
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            // Apply a spring-like force to bounce back
            Vector3 bounceDirection = initialPosition - transform.position;
            bounceDirection.y = 0;
            bounceDirection.z = 0;
            rb.AddForce(bounceDirection * originalSpring, ForceMode.Impulse);
        }
    }
}